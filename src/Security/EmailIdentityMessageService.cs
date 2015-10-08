using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PrekenWeb.Security.Properties;

namespace PrekenWeb.Security
{ 
    public class EmailIdentityMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var smtpServer = ConfigurationManager.AppSettings["SMTPServer"];

            using (SmtpClient smtpClient = new SmtpClient(smtpServer))
            {
                smtpClient.UseDefaultCredentials = true;

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress("info@prekenweb.nl", "PrekenWeb"),
                    Subject = message.Subject,
                    IsBodyHtml = true,
                    Body = message.Body
                };
                mailMessage.To.Add(message.Destination);
                //message.To.Add(new MailAddress(opvolging.Inbox.VanEmail, opvolging.Inbox.VanNaam));
                await smtpClient.SendMailAsync(mailMessage);
            } 
        }
    }
}