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
            using (SmtpClient smtpClient = new SmtpClient(Settings.Default.SMTPServer))
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