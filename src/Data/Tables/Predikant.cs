using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Tables
{
    public class Predikant
    {
        public int Id { get; set; }  
        public string Titels { get; set; }  
        public string Voorletters { get; set; }  
        public string Achternaam { get; set; } 
        public string Gemeente { get; set; }  
        public string LevensPeriode { get; set; }  
        public int? OudId { get; set; } 
        public int? GemeenteId { get; set; }  
        public string Tussenvoegsels { get; set; } 

        [DataType(DataType.Html)]
        public string Opmerking { get; set; }

        [UIHint("Taal"), Display(Name = "Taal", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(ErrorMessage = "Taal is verplicht!")]
        public int TaalId { get; set; }  

        [NotMapped]
        public string VolledigeNaam
        {
            get
            {
                return string.Format("{0} {1} {2} {3}", Titels, Voorletters, Tussenvoegsels, Achternaam).Replace("  ", " ");
            }
            set { }
        }

        public virtual ICollection<Preek> Preeks { get; set; } 

        public virtual Gemeente Gemeente_GemeenteId { get; set; } 
        public virtual Taal Taal { get; set; } 

        public virtual bool HideFromIndexingRobots { get; set; }
        public virtual bool HideFromPodcast { get; set; }

        public Predikant()
        {
            TaalId = 1;
            Preeks = new List<Preek>();
            
        }
    }

}
