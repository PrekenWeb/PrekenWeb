using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Hosting;
using Business.Helpers;
using Data;
using Hangfire;
using log4net;
using NAudio.FileFormats.Mp3;
using NAudio.Wave;

namespace Prekenweb.Website.Lib.Hangfire
{
    public class AchtergrondTaken
    {
        private readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void RegistreerTaken()
        {
            RecurringJob.AddOrUpdate<AchtergrondTaken>("InboxSamenvattingTaak", x => x.InboxSamenvattingTaak(), Cron.Daily(1, 0));
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void HerstelWerkzaamheden()
        {
            using (var context = new PrekenwebContext())
            {
                var prekenZonderTijd = context.Preeks.Where(x => !x.Duur.HasValue).Select(x => x.Id).ToArray();
                foreach (var id in prekenZonderTijd)
                {
                    BackgroundJob.Enqueue<AchtergrondTaken>(x => x.AnalyseerAudioTaak(id));
                }

                var inboxOpvolgingIds = context
                    .InboxOpvolgings
                    .Where(x => !x.Verstuurd.HasValue && x.VerstuurAlsMail && x.GebruikerId.HasValue)
                    .Select(x => x.Id)
                    .Distinct()
                    .ToArray();

                foreach (var id in inboxOpvolgingIds)
                {
                    BackgroundJob.Enqueue<AchtergrondTaken>(x => x.InboxOpvolgingTaak(id));
                }
            }
        }

        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void AnalyseerAudioTaak(int id)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["TestEnvironement"])) return;

            using (var context = new PrekenwebContext())
            {
                var preek = context.Preeks.Single(x => x.Id == id);

                var filename = HostingEnvironment.MapPath(Path.Combine(ConfigurationManager.AppSettings["PrekenFolder"], preek.Bestandsnaam));
                if (!File.Exists(filename) || Path.GetExtension(filename) != ".mp3")
                {
                    _logger.Warn("Verwerking preek overgeslagen: geen (MP3) bestand gevonden");
                    return;
                }
                try
                {
                    using (var fr = new Mp3FileReader(filename, mp3Format => new DmoMp3FrameDecompressor(mp3Format)))
                    {
                        preek.Duur = fr.TotalTime;
                        preek.Bestandsgrootte = (int)new FileInfo(filename).Length;
                        context.SaveChanges();
                    }
                }
                catch (COMException ex)
                {
                    throw new Exception("Problem with processing audio of preek, probably required to configure codec on server or install 'desktop experience'? http://mark-dot-net.blogspot.nl/2014/04/nodriver-calling-acmformatsuggest.html", ex);
                }
            }
        }

        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void InboxOpvolgingTaak(int inboxOpvolgingId)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["TestEnvironement"])) return;

            using (var context = new PrekenwebContext())
            {
                var opvolging = context.InboxOpvolgings.Single(x => x.Id == inboxOpvolgingId);

                opvolging.Verstuurd = DateTime.Now;
                context.SaveChanges();

                try
                {
                    using (var smtpClient = SmtpHelper.GetSmtpClient())
                    {
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
                catch (Exception ex)
                {
                    _logger.Warn("Versturen inbox opvolgings mail mislukt", ex);
                    opvolging.Verstuurd = null;
                    context.SaveChanges();
                    throw;
                }
            }
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void InboxSamenvattingTaak()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["TestEnvironement"])) return;

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
                    sb.Append("<center><img src='https://www.prekenweb.nl/Content/Images/LogoKlein.png'/>");
                    sb.Append("<h1 style='font-family:arial; font-size:16px; color:#1274BA;' color='#1274BA'>Samenvatting mailcontact via PrekenWeb.nl</h1></center></td></tr><tr><td>");

                    foreach (var inbox in ontvanger)
                    {
                        sb.Append("<div style='border:2px solid #1274BA; background-color:#F2F3F8; margin:0 auto; padding:10px; width:500px; font-family:arial; font-size:12px;'>");
                        sb.Append($"<b>Ontvangen:</b> {inbox.Aangemaakt:g}<br/>");
                        sb.Append($"<b>Van:</b> {inbox.VanNaam} {inbox.VanEmail}<br/>");
                        sb.Append($"<b>Reden:</b> {inbox.InboxType.Omschrijving}<br/>");
                        sb.Append($"<b>Onderwerp:</b> {inbox.Omschrijving}<br/><hr style='border:1px solid gray;' />");
                        sb.Append($"{inbox.Inhoud}");
                        var i = 1;
                        foreach (var opvolging in inbox.InboxOpvolgings)
                        {
                            sb.Append("<div style='border:1px solid gray; margin:10px; padding:10px;'>");
                            sb.Append($"<h2 style='font-family:arial; font-size:14px; color:#1274BA;' color='#1274BA'>Opvolging {i++}</h2>");
                            sb.Append($"<b>Aangemaakt:</b> {opvolging.Aangemaakt:g}<br/>");
                            sb.Append($"<b>Als mail versturen?:</b> {opvolging.VerstuurAlsMail}<br/>");
                            sb.Append($"<b>Verstuurd:</b> {opvolging.Verstuurd:g}<br/>");
                            if (opvolging.GebruikerId.HasValue)
                                sb.Append($"<b>Door:</b> {opvolging.Gebruiker.Naam}<br/>");
                            sb.Append($"<b>Aan:</b> {opvolging.Inbox.VanNaam} {opvolging.Inbox.VanEmail}<br/>");
                            sb.Append($"<b>Onderwerp:</b> {opvolging.Onderwerp}<br/><hr style='border:1px solid gray;' />");
                            sb.Append($"{opvolging.Tekst}");
                            sb.Append("</div>");
                        }
                        sb.Append("</div><br/><br/>");
                    }
                    sb.Append("</td></tr></table>");

                    try
                    {
                        using (var smtpClient = SmtpHelper.GetSmtpClient())
                        {
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
                    catch (Exception ex)
                    {
                        _logger.Warn("Versturen samenvatting mailcontact mislukt", ex);
                        throw;
                    }
                }
            }
        }
    }
}
