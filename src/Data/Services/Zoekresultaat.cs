using System.Collections.Generic;
using System.Linq;
using Data.Tables;
using Data.ViewModels;

namespace Data.Services
{
    public class Zoekresultaat
    {
        public ZoekOpdracht ZoekOpdracht { get; set; }
        public IEnumerable<ZoekresultaatItem> Items { get; set; }
        public int AantalResultaten { get; set; }

        public bool HideFromIndexingRobots
        {
            get
            {
                if (Items == null || !Items.Any())
                    return false;

                return Items.Any(i => i.Preek?.Predikant != null && i.Preek.Predikant.HideFromIndexingRobots);
            }
        }

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