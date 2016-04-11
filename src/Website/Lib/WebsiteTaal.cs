using System.Globalization;

namespace Prekenweb.Website.Lib
{
    public class WebsiteTaal
    {
        public WebsiteTaal(string naam, int id)
        {
            Naam = naam;
            Id = id;
            CultureInfo = new CultureInfo(naam);
        }

        public string Naam { get; set; }
        public int Id { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}