using System;
using Newtonsoft.Json;
using PrekenWeb.Data.Identity;

namespace PrekenWeb.Data.Tables
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

        [JsonIgnore]
        public virtual Gebruiker Gebruiker { get; set; }
       
        [JsonIgnore]
        public virtual Preek Preek { get; set; }  
    }

}
