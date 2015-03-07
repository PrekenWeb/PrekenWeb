using Prekenweb.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prekenweb.Models
{
    public class Inbox
    { 
        public int Id { get; set; } 
       
        public int InboxTypeId { get; set; }  
       
        public string Omschrijving { get; set; }  

        [DataType(DataType.Html), DisplayName("Inhoud")]
        public string Inhoud { get; set; } 
        
        [UIHint("Gebruiker"), DisplayName("Door")]
        public int? GebruikerId { get; set; } 
        
        public bool Afgehandeld { get; set; } 
        
        public DateTime Aangemaakt { get; set; }  

        [DisplayName("Verstuurd door")]
        public string VanNaam { get; set; }  

        [DisplayName("Van email"), DataType(DataType.EmailAddress)]
        public string VanEmail { get; set; } 

        [DisplayName("Verstuurd aan"), DefaultValue("PrekenWeb")]
        public string AanNaam { get; set; }

        [DisplayName("Aan email"), DataType(DataType.EmailAddress), DefaultValue("info@prekenweb.nl")]
        public string AanEmail { get; set; }  

        [DisplayName("Gerelateerde preek")]
        public int? PreekId { get; set; }  

        public virtual ICollection<InboxOpvolging> InboxOpvolgings { get; set; }  

        public virtual Gebruiker Gebruiker { get; set; }  
        public virtual InboxType InboxType { get; set; }  
        public virtual Preek Preek { get; set; }  

        public Inbox()
        {
            Afgehandeld = false;
            Aangemaakt = DateTime.Now;
            InboxOpvolgings = new List<InboxOpvolging>();
            AanEmail = "info@prekenweb.nl";
            AanNaam = "PrekenWeb";
            
        }
        
    }

}
