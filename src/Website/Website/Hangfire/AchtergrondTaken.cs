using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;
using Hangfire;
using NAudio.FileFormats.Mp3;
using NAudio.Wave;
using Prekenweb.Models;
using Prekenweb.Website.Properties;

namespace Prekenweb.Website.Hangfire
{
    public class AchtergrondTaken
    {
        public static void RegistreerTaken()
        {
            RecurringJob.AddOrUpdate<AchtergrondTaken>("InboxSamenvattingTaak", x => x.InboxSamenvattingTaak(), Cron.Daily(1, 0));
        }

        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void AnalyseerAudioTaak(int id)
        {
            using (var context = new PrekenwebContext())
            {
                var preek = context.Preeks.Single(x => x.Id == id);
                
                var filename = HostingEnvironment.MapPath(Path.Combine(Settings.Default.PrekenFolder, preek.Bestandsnaam));
                if (!File.Exists(filename) || Path.GetExtension(filename) != ".mp3") return;

                using (var fr = new Mp3FileReader(filename, mp3Format => new DmoMp3FrameDecompressor(mp3Format)))
                {
                    preek.Duur = fr.TotalTime;
                    preek.Bestandsgrootte = (int)new FileInfo(filename).Length; 
                    context.SaveChanges();
                }
            }
        }

        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void InboxOpvolgingTaak(int inboxOpvolgingId)
        {
            using (var context = new PrekenwebContext())
            {
                var opvolging = context.InboxOpvolgings.Single(x => x.Id == inboxOpvolgingId);

                opvolging.Verstuurd = DateTime.Now;
                context.SaveChanges();

                try
                {
                    using (var smtpClient = new SmtpClient(Settings.Default.SMTPServer))
                    {
                        smtpClient.UseDefaultCredentials = true;

                        var message = new MailMessage
                        {
                            From = new MailAddress("info@prekenweb.nl", "PrekenWeb"),
                            Subject = opvolging.Onderwerp,
                            IsBodyHtml = true,
                            Body = opvolging.Tekst
                        };

                        message.To.Add(new MailAddress(opvolging.Inbox.VanEmail, opvolging.Inbox.VanNaam));
                        smtpClient.Send(message);
                    }
                }
                catch //(Exception ex)
                {
                    opvolging.Verstuurd = null;
                    context.SaveChanges();
                    throw;
                }
            }
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void InboxSamenvattingTaak()
        {
            using (var context = new PrekenwebContext())
            {
                var samenvattingVanaf = DateTime.Now.AddDays(-1);
                var inboxItems = context
                    .Inboxes
                    .Include(x => x.InboxOpvolgings)
                    .Where(i =>
                        i.Aangemaakt > samenvattingVanaf
                        || i.InboxOpvolgings.Any(io => io.Verstuurd > samenvattingVanaf)
                        || i.InboxOpvolgings.Any(io => io.Aangemaakt > samenvattingVanaf)
                    )
                    .GroupBy(i => i.AanEmail)
                    .ToList();

                if (!inboxItems.Any()) return;

                foreach (var ontvanger in inboxItems)
                { 
                    var sb = new StringBuilder();

                    sb.Append("<table width=500 style='width:500px;'><tr><td width=500 style='width:500px; text-align:center;'>");
                    sb.Append("<center><img src='http://www.prekenweb.nl/Content/Images/LogoKlein.png'/>");
                    sb.Append("<h1 style='font-family:arial; font-size:16px; color:#1274BA;' color='#1274BA'>Samenvatting mailcontact via PrekenWeb.nl</h1></center></td></tr><tr><td>");

                    foreach (var inbox in ontvanger)
                    {
                        sb.Append("<div style='border:2px solid #1274BA; background-color:#F2F3F8; margin:0 auto; padding:10px; width:500px; font-family:arial; font-size:12px;'>");
                        sb.AppendFormat("<b>Ontvangen:</b> {0:g}<br/>", inbox.Aangemaakt);
                        sb.AppendFormat("<b>Van:</b> {0} {1}<br/>", inbox.VanNaam, inbox.VanEmail);
                        sb.AppendFormat("<b>Reden:</b> {0}<br/>", inbox.InboxType.Omschrijving);
                        sb.AppendFormat("<b>Onderwerp:</b> {0}<br/><hr style='border:1px solid gray;' />", inbox.Omschrijving);
                        sb.AppendFormat("{0}", inbox.Inhoud);
                        var i = 1;
                        foreach (var opvolging in inbox.InboxOpvolgings)
                        {
                            sb.Append("<div style='border:1px solid gray; margin:10px; padding:10px;'>");
                            sb.AppendFormat("<h2 style='font-family:arial; font-size:14px; color:#1274BA;' color='#1274BA'>Opvolging {0}</h2>", i++);
                            sb.AppendFormat("<b>Aangemaakt:</b> {0:g}<br/>", opvolging.Aangemaakt);
                            sb.AppendFormat("<b>Als mail versturen?:</b> {0}<br/>", opvolging.VerstuurAlsMail);
                            sb.AppendFormat("<b>Verstuurd:</b> {0:g}<br/>", opvolging.Verstuurd);
                            if (opvolging.GebruikerId.HasValue)
                                sb.AppendFormat("<b>Door:</b> {0}<br/>", opvolging.Gebruiker.Naam);
                            sb.AppendFormat("<b>Aan:</b> {0} {1}<br/>", opvolging.Inbox.VanNaam, opvolging.Inbox.VanEmail);
                            sb.AppendFormat("<b>Onderwerp:</b> {0}<br/><hr style='border:1px solid gray;' />", opvolging.Onderwerp);
                            sb.AppendFormat("{0}", opvolging.Tekst);
                            sb.Append("</div>");
                        }
                        sb.Append("</div><br/><br/>");
                    }
                    sb.Append("</td></tr></table>");


                    using (var smtpClient = new SmtpClient(Settings.Default.SMTPServer))
                    {
                        smtpClient.UseDefaultCredentials = true;

                        var message = new MailMessage
                        {
                            From = new MailAddress("info@prekenweb.nl", "PrekenWeb"),
                            Subject = "Samenvatting mailcontact",
                            IsBodyHtml = true,
                            Body = sb.ToString()
                        };
                        message.To.Add(new MailAddress(ontvanger.First().AanEmail, ontvanger.First().AanNaam));
                        smtpClient.Send(message);
                    } 
                }
            }
        }
    }
}
