using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Tables
{
    public class Serie
    {
        public int Id { get; set; }  

        [Display(Name = "Omschrijving", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Omschrijving { get; set; } 
 
        public int? OudId { get; set; } 

        [UIHint("Taal"), Display(Name = "Taal", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(ErrorMessage = "Taal is verplicht!")]
        public int TaalId { get; set; }  

        public virtual ICollection<Preek> Preeks { get; set; }  
        public virtual Taal Taal { get; set; }  

        public Serie()
        {
            TaalId = 1;
            Preeks = new List<Preek>(); 
        }
    } 
}
