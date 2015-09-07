using System.Collections.Generic;

namespace PrekenWeb.Data.Tables
{ 
    public class Afbeelding
    {
        public int Id { get; set; }  
        public string Omschrijving { get; set; } 
        public string Bestandsnaam { get; set; } 
        public string ContentType { get; set; }   
         
        public virtual ICollection<Preek> Preeks { get; set; } 
        public virtual ICollection<Spotlight> Spotlights { get; set; } 

        public Afbeelding()
        {
            Preeks = new List<Preek>();
            Spotlights = new List<Spotlight>();
            
        }
    }

}
