using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Tables
{
    public class Gebeurtenis
    {
        public int Id { get; set; }
        [Display(Name = "EventDescription", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public string Omschrijving { get; set; }
        public int? OudId { get; set; }
        [Display(Name = "EventSorting", ResourceType = typeof(Prekenweb.Resources.Resources))]
        public int Sortering { get; set; }

        [UIHint("Taal"), Display(Name = "Taal", ResourceType = typeof(Prekenweb.Resources.Resources)), Required(ErrorMessage = "Taal is verplicht!")]
        public int TaalId { get; set; }


        public virtual ICollection<Preek> Preeks { get; set; }
        public virtual Taal Taal { get; set; }

        public Gebeurtenis()
        {
            Sortering = 0;
            TaalId = 1;
            Preeks = new List<Preek>();
        }
    }

}
