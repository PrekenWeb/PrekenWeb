using System.Globalization;

namespace Prekenweb.Website.Lib
{
    public class WebsiteTaal
    {
        public WebsiteTaal(string hostName, string naam, int id)
        {
            Hostname = hostName;
            Naam = naam;
            Id = id;
            CultureInfo = new CultureInfo(naam);
        }

        public string Hostname { get; set; }
        public string Naam { get; set; }
        public int Id { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}