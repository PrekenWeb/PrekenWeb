using System.Collections.Generic;

namespace PrekenWeb.Data.Tables
{ 
    public class Boek
    {
        public int Id { get; set; } 
        public string Boeknaam { get; set; } 
        public int Sortering { get; set; } 
        public int? OudId { get; set; } 
        public string Afkorting { get; set; } 
        public bool ToonHoofdstukNummer { get; set; }  
        public int TaalId { get; set; } 
         
        public virtual ICollection<BoekHoofdstuk> BoekHoofdstuks { get; set; }  
         
        public virtual Taal Taal { get; set; } 

        public Boek()
        {
            Sortering = 0;
            ToonHoofdstukNummer = true;
            TaalId = 1;
            BoekHoofdstuks = new List<BoekHoofdstuk>();
            
        }
        
    }

}
