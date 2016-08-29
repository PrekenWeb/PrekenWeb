using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PrekenWeb.Data.Smtp;

namespace PrekenWeb.Security
{ 
    public class EmailIdentityMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var smtpServer = ConfigurationManager.AppSettings["SMTPServer"];

            using (SmtpClient smtpClient = SmtpHelper.GetSmtpClient())
            { 

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress("info@prekenweb.nl", "PrekenWeb"),
                    Subject = message.Subject,
                    IsBodyHtml = true,
                    Body = message.Body
                };
                mailMessage.To.Add(message.Destination);
                //message.To.Add(new MailAddress(opvolging.Inbox.VanEmail, opvolging.Inbox.VanNaam));
                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    Debug.Write(ex.Message);
                    throw;
                }
            } 
        }

       
    }

}