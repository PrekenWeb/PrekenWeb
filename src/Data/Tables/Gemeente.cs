using System.Collections.Generic;

namespace PrekenWeb.Data.Tables
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
