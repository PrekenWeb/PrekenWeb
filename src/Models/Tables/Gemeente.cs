using System.Collections.Generic;

namespace Prekenweb.Models
{
    public class Gemeente
    {
        public int Id { get; set; } 
        public string Omschrijving { get; set; } 

        public virtual ICollection<Predikant> Predikants { get; set; }  
        public virtual ICollection<Preek> Preeks { get; set; } 

        public Gemeente()
        {
            Predikants = new List<Predikant>();
            Preeks = new List<Preek>(); 
        }  
    }

}
