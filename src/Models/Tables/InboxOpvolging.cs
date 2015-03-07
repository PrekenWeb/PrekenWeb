using Prekenweb.Models.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prekenweb.Models
{
    public class InboxOpvolging
    {
         
        public int Id { get; set; }  
      
        public int InboxId { get; set; }  
       
        [UIHint("Gebruiker"), DisplayName("Gebruiker")]
        public int? GebruikerId { get; set; }  
      
        public DateTime Aangemaakt { get; set; }  
      
        public string Onderwerp { get; set; }  
       
        [DataType(DataType.Html), DisplayName("Tekst")]
        public string Tekst { get; set; } 
       
        [DisplayName("Verstuur als mail")]
        public bool VerstuurAlsMail { get; set; }  
                                                   
        public DateTime? Verstuurd { get; set; }   
         
        public virtual Gebruiker Gebruiker { get; set; }  
        public virtual Inbox Inbox { get; set; }  
    }

}
