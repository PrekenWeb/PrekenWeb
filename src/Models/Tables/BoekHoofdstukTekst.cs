using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prekenweb.Models
{
    public class BoekHoofdstukTekst
    {
        public int Id { get; set; } 
        public int BoekHoofdstukId { get; set; }  
        public int Hoofdstuk { get; set; }  
        public int Vers { get; set; } 
        public string Tekst { get; set; }  
        public int Sortering { get; set; }  

        [NotMapped]
        public string Omschrijving
        {
            get
            {
                return string.Format("{0} {1} : {2}", BoekHoofdstuk.Omschrijving, Hoofdstuk, Vers);
            }
        } 
        
        public virtual ICollection<Preek> Preeks_GedeelteTotVersId { get; set; }  
        public virtual ICollection<Preek> Preeks_GedeelteVanVersId { get; set; }  
        public virtual ICollection<Preek> Preeks_VersTotId { get; set; }  
        public virtual ICollection<Preek> Preeks_VersVanId { get; set; }   
        
        public virtual BoekHoofdstuk BoekHoofdstuk { get; set; } 

        public BoekHoofdstukTekst()
        {
            Sortering = 0;
            Preeks_GedeelteTotVersId = new List<Preek>();
            Preeks_GedeelteVanVersId = new List<Preek>();
            Preeks_VersTotId = new List<Preek>();
            Preeks_VersVanId = new List<Preek>();
            
        }
        
    }

}
