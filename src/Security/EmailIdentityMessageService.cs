using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PrekenWeb.Security
{ 
    public class EmailIdentityMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var smtpServer = ConfigurationManager.AppSettings["SMTPServer"];

            using (SmtpClient smtpClient = GetSmtpClient())
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
                await smtpClient.SendMailAsync(mailMessage);
            } 
        }

        public SmtpClient GetSmtpClient()
        {
            var host = ConfigurationManager.AppSettings["SMTPServer.Host"];
            var port = int.Parse(ConfigurationManager.AppSettings["SMTPServer.Port"]);
            var userName = ConfigurationManager.AppSettings["SMTPServer.Username"];
            var password = ConfigurationManager.AppSettings["SMTPServer.Password"];
            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = port != 25;
            smtpClient.Credentials = new NetworkCredential(userName, password);
            return smtpClient;
        }
    }
}