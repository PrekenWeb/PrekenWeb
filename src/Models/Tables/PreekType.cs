using System.Collections.Generic;

namespace Prekenweb.Models
{
    public class PreekType
    {
        public int Id { get; set; }  
        public string Omschrijving { get; set; }  

        public virtual ICollection<Preek> Preeks { get; set; } 

        public PreekType()
        {
            Preeks = new List<Preek>(); 
        }
    }

}
