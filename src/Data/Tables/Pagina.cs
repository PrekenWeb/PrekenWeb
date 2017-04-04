using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Data.Attributes;
using Data.Identity;

namespace Data.Tables
{
    public class Pagina
    {
        public int Id { get; set; } 
      
        [Tooltip("Mag iedere (ook niet ingelogde) bezoeker deze pagina al zien?")]
        public bool Gepubliceerd { get; set; } 
     
        [Required, Display(Name = "URL code"), Tooltip("Tekstuele (unieke) waarde waarmee de pagina wordt gekenmerkt in de URL.<br/>Dit wordt als volgt onderdeel van de URL: http://www.prekenweb.nl/nl/pagina/<u>paginanaam</u>")]
        public string Identifier { get; set; }  
     
        [DisplayName("Aangemaakt op")]
        public DateTime Aangemaakt { get; set; } 
       
        [DisplayName("Bijgewerkt op")]
        public DateTime Bijgewerkt { get; set; } 
       
        [UIHint("Gebruiker"), DisplayName("Aangemaakt door")]
        public int AangemaaktDoor { get; set; } 
        
        [UIHint("Gebruiker"), DisplayName("Bijgewerkt door")]
        public int BijgewerktDoor { get; set; }  
        
        [DisplayName("Tonen op homepage"), Tooltip("Door dit vinkje aan te zetten komt deze pagina op de homepage in het lijstje met nieuwe pagina's/artikelen, er wordt gesorteerd op datum bijgewerkt.")]
        public bool TonenOpHomepage { get; set; }  

        public virtual ICollection<Tekst> Teksts { get; set; }  

        public virtual Gebruiker Gebruiker_AangemaaktDoor { get; set; }  
        public virtual Gebruiker Gebruiker_BijgewerktDoor { get; set; }  

        public Pagina()
        {
            Gepubliceerd = false;
            Aangemaakt = DateTime.Now;
            Bijgewerkt = DateTime.Now; 
            TonenOpHomepage = false;
            Teksts = new List<Tekst>(); 
        }
        

        public string GetKop(int taalId)
        {
            var alleTeksten = Teksts.ToList();
            if (alleTeksten.Any(t => t.TaalId == taalId)) return alleTeksten.Single(t => t.TaalId == taalId).Kop;
            if (alleTeksten.Any()) return alleTeksten.First().Kop;
            return Identifier;
        }
    }

}
