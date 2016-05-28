using System.Collections.Generic;

namespace PrekenWeb.Data.Tables
{ 
    public class BoekHoofdstuk
    {
        public int Id { get; set; }  
        public int BoekId { get; set; }  
        public string Omschrijving { get; set; }   
        public int? Sortering { get; set; }  
        public int? OudId { get; set; }  

        
        public virtual ICollection<BoekHoofdstukTekst> BoekHoofdstukTeksts { get; set; }  
        public virtual ICollection<Preek> Preeks { get; set; }  

        
        public virtual Boek Boek { get; set; }  

        public BoekHoofdstuk()
        {
            BoekHoofdstukTeksts = new List<BoekHoofdstukTekst>();
            Preeks = new List<Preek>(); 
        } 

    }

}
