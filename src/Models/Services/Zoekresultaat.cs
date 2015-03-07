using System.Collections.Generic;

namespace Prekenweb.Models.Services
{
    public class Zoekresultaat
    {
        public ZoekOpdracht ZoekOpdracht { get; set; }
        public IEnumerable<ZoekresultaatItem> Items { get; set; }
        public int AantalResultaten { get; set; }

        public Zoekresultaat()
        {
            ZoekOpdracht = new ZoekOpdracht();
            Items = new List<ZoekresultaatItem>();
        }

        public Zoekresultaat(ZoekOpdracht zoekOpdracht,IEnumerable<ZoekresultaatItem> items )
        {
            ZoekOpdracht = zoekOpdracht;
            Items = items;
        }
    }
}