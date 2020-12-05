namespace PrekenWeb.Security.Helpers
{
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;

    public static class SmtpHelper
    {
        public static SmtpClient GetSmtpClient()
        {
            // TODO (I)ConfigurationManager(Wrapper)
            var host = ConfigurationManager.AppSettings["SMTPServer.Host"];
            var port = int.Parse(ConfigurationManager.AppSettings["SMTPServer.Port"]);
            var userName = ConfigurationManager.AppSettings["SMTPServer.Username"];
            var password = ConfigurationManager.AppSettings["SMTPServer.Password"];

            var smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = port != 25
            };

            if (string.IsNullOrEmpty(userName))
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(userName, password);
            }
            return smtpClient;
        }
    }
}
