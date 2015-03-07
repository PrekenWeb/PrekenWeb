using System.Collections.Generic;

namespace Prekenweb.Models
{
    public class Taal
    {
        public int Id { get; set; } 
        public string Code { get; set; } 
        public string Omschrijving { get; set; } 

        public virtual ICollection<Boek> Boeks { get; set; }  
        public virtual ICollection<Gebeurtenis> Gebeurtenis { get; set; }  
        public virtual ICollection<Mailing> Mailings { get; set; }  
        public virtual ICollection<NieuwsbriefInschrijving> NieuwsbriefInschrijvings { get; set; } 
        public virtual ICollection<Predikant> Predikants { get; set; } 
        public virtual ICollection<Preek> Preeks { get; set; }  
        public virtual ICollection<Serie> Series { get; set; } 
        public virtual ICollection<Spotlight> Spotlights { get; set; }  
        public virtual ICollection<Tekst> Teksts { get; set; }  

        public Taal()
        {
            Boeks = new List<Boek>();
            Gebeurtenis = new List<Gebeurtenis>();
            Mailings = new List<Mailing>();
            NieuwsbriefInschrijvings = new List<NieuwsbriefInschrijving>();
            Predikants = new List<Predikant>();
            Preeks = new List<Preek>();
            Series = new List<Serie>();
            Spotlights = new List<Spotlight>();
            Teksts = new List<Tekst>();
            
        }
    }

}
