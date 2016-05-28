using System.Globalization;

namespace Prekenweb.Website.Lib
{
    public class WebsiteTaal
    {
        public WebsiteTaal(string[] hostNames, string naam, int id)
        {
            Hostnames = hostNames;
            Naam = naam;
            Id = id;
            CultureInfo = new CultureInfo(naam);
        }

        public string[] Hostnames { get; set; }
        public string Naam { get; set; }
        public int Id { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}