using Prekenweb.Models.Identity;
using System;

namespace Prekenweb.Models
{
    public class PreekCookie
    {
        public int Id { get; set; }  
        public int PreekId { get; set; } 
        public DateTime? DateTime { get; set; } 
        public string Opmerking { get; set; }
        public DateTime? BladwijzerGelegdOp { get; set; }
        public TimeSpan? AfgespeeldTot { get; set; }  
        public int GebruikerId { get; set; }  

        public virtual Gebruiker Gebruiker { get; set; } 
        public virtual Preek Preek { get; set; }  
    }

}
