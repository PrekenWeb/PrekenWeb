using System.Text;
using System.Threading.Tasks;
using Prekenweb.Models.Repository;
using PrekenWeb.Security;

namespace Prekenweb.Website.Lib.MailTemplating
{
    public class MailTemplating
    {
        private static MailTemplating _mailTemplating;
        private IGebruikerRepository _gebruikerRepository;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        private MailTemplating(IGebruikerRepository gebruikerRepository, IPrekenWebUserManager prekenWebUserManager)
        {
            _gebruikerRepository = gebruikerRepository;
            _prekenWebUserManager = prekenWebUserManager;
        }

        public static MailTemplating GetMailTemplating(IGebruikerRepository gebruikerRepository, IPrekenWebUserManager prekenWebUserManager)
        {
            if (_mailTemplating == null) _mailTemplating = new MailTemplating(gebruikerRepository, prekenWebUserManager);
            return _mailTemplating;
        }

        public async Task<string> GetWachtwoordVergetenMailBody(int gebruikerId, string onderwerp, string callbackUrl)
        {
            var gebruiker = await _prekenWebUserManager.FindByIdAsync(gebruikerId);

            StringBuilder sb = new StringBuilder();
            sb.Append(getHeader(onderwerp));
            sb.AppendFormat("<p>Beste {0}</p>", gebruiker.Naam);
            sb.AppendFormat("<p>Via PrekenWeb.nl is een nieuw wachtwoord aangevraagd voor dit email adres</p>");
            sb.AppendFormat("<p>Uw gebruikersnaam is {0}</p>", gebruiker.UserName);
            sb.AppendFormat("<p>Klik <a href=\"" + callbackUrl + "\">hier</a> om een nieuw wachtwoord in te stellen</p>");
            sb.AppendFormat("<p>Of als u geen link ziet, plak onderstaande link in uw browser:</p> <a href=\"" + callbackUrl + "\">hier</a> om een nieuw wachtwoord in te stellen</p>");
            sb.AppendFormat("<p>" + callbackUrl + "</p>");
            sb.AppendFormat("<p>Met vriendelijke groet,</p>");
            sb.AppendFormat("<p>PrekenWeb</p>");
            sb.Append(getFooter());
            return sb.ToString();
        }

        private string getHeader(string onderwerp)
        {
            StringBuilder sb = new StringBuilder();
            //
            sb.Append("<table width=500 style='width:500px;'><tr><td width=500 style='width:500px; text-align:center;'>");
            sb.Append("<center><img src='http://www.prekenweb.nl/Content/Images/LogoKlein.png'/>");
            sb.AppendFormat("<h1 style='font-family:arial; font-size:16px; color:#1274BA;' color='#1274BA'>{0}</h1></center></td></tr><tr><td style=' font-family:arial; font-size:12px;'>", onderwerp);

            sb.Append("</td></tr></table>");
            return sb.ToString();
        }
        private string getFooter( )
        {
            StringBuilder sb = new StringBuilder(); 
            sb.Append("</td></tr></table>");
            return sb.ToString();
        } 
    }
}