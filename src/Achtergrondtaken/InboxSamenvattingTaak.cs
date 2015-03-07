using Achtergrondtaken.Properties;
using Prekenweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Achtergrondtaken
{
    public class InboxSamenvattingTaak
    {
        public static DateTime SamenvattingVanaf = DateTime.Now.AddDays(-1);

        internal void Start()
        {
            using (PrekenwebContext _context = new PrekenwebContext())
            {
                var inboxItems = _context
                    .Inboxes
                    .Include(x => x.InboxOpvolgings)
                    .Where(i =>
                        i.Aangemaakt > SamenvattingVanaf
                        || i.InboxOpvolgings.Any(io => io.Verstuurd > SamenvattingVanaf)
                        || i.InboxOpvolgings.Any(io => io.Aangemaakt > SamenvattingVanaf)
                    )
                    .ToList()
                    .GroupBy(i => i.AanEmail);
                if (inboxItems.Count() == 0) return;


                foreach (var ontvanger in inboxItems)
                {
                    Console.WriteLine("{0}", ontvanger.Key);
                    StringBuilder sb = new StringBuilder();
                    //
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
                        int i = 1;
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

                    try
                    {
                        using (SmtpClient smtpClient = new SmtpClient(Settings.Default.SMTPServer))
                        {
                            smtpClient.UseDefaultCredentials = true;

                            MailMessage message = new MailMessage()
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
                    catch //(Exception ex)
                    {
                    }
                }
            }
        }
    }
}
