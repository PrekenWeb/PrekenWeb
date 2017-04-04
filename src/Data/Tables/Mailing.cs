using System.Collections.Generic;
using Data.Identity;

namespace Data.Tables
{
    public class Mailing
    {
        public Mailing()
        {
            Gebruikers = new List<Gebruiker>(); 
        }

        public int Id { get; set; } 
        public string Omschrijving { get; set; }  
        public string MailChimpId { get; set; }  
        public int TaalId { get; set; }  

        public virtual ICollection<Gebruiker> Gebruikers { get; set; }  

        public virtual Taal Taal { get; set; }
    }

}
