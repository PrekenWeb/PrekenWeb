using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;
using Business.Helpers;
using Microsoft.AspNet.Identity;

namespace PrekenWeb.Security
{ 
    public class EmailIdentityMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            //var smtpServer = ConfigurationManager.AppSettings["SMTPServer"];

            using (var smtpClient = SmtpHelper.GetSmtpClient())
            { 

                var mailMessage = new MailMessage
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