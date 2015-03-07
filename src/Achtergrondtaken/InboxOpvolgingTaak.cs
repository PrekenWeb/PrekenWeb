using Achtergrondtaken.Properties;
using Prekenweb.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;

namespace Achtergrondtaken
{
    public class InboxOpvolgingTaak
    {

        internal void Start()
        {
            using (PrekenwebContext _context = new PrekenwebContext())
            {
                foreach (var opvolging in _context.InboxOpvolgings.Include(x => x.Inbox).Where(io => io.VerstuurAlsMail && !io.Verstuurd.HasValue).ToList())
                {
                    opvolging.Verstuurd = DateTime.Now;
                    _context.SaveChanges();

                    try
                    {
                        using (SmtpClient smtpClient = new SmtpClient(Settings.Default.SMTPServer))
                        {
                            smtpClient.UseDefaultCredentials = true;
                            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //smtpClient.Credentials = new NetworkCredential(Settings.GetSettings()["SMTP_GEBRUIKERSNAAM"], Settings.GetSettings()["SMTP_WACHTWOORD"], Settings.GetSettings()["SMTP_DOMEIN"]);

                            MailMessage message = new MailMessage()
                            {
                                From = new MailAddress("info@prekenweb.nl", "PrekenWeb"),
                                Subject = opvolging.Onderwerp,
                                IsBodyHtml = true,
                                Body = opvolging.Tekst
                            };

                            //using (Attachment att = new Attachment(file))
                            //{
                            //    att.ContentDisposition.FileName = Path.GetFileName(file);
                            //    message.Attachments.Add(att);

                            //    message.To.Add(new MailAddress(Settings.GetSettings()["InvoiceNotificationReceiverEmail"], Settings.GetSettings()["InvoiceNotificationReceiverName"]));
                            //}
                            //message.To.Add(new MailAddress("k.schollaart@aspect-ict.nl", "Kees Schollaart"));
                            message.To.Add(new MailAddress(opvolging.Inbox.VanEmail, opvolging.Inbox.VanNaam));
                            smtpClient.Send(message);
                        }
                    }
                    catch //(Exception ex)
                    {
                        opvolging.Verstuurd = (DateTime?)null;
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}