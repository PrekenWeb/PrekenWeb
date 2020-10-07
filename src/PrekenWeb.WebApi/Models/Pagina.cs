using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Pagina
    {
        public Pagina()
        {
            Tekst = new HashSet<Tekst>();
        }

        public int Id { get; set; }
        public bool Gepubliceerd { get; set; }
        public string Identifier { get; set; }
        public DateTime Aangemaakt { get; set; }
        public DateTime Bijgewerkt { get; set; }
        public int AangemaaktDoor { get; set; }
        public int BijgewerktDoor { get; set; }
        public bool TonenOpHomepage { get; set; }

        public virtual Gebruiker AangemaaktDoorNavigation { get; set; }
        public virtual Gebruiker BijgewerktDoorNavigation { get; set; }
        public virtual ICollection<Tekst> Tekst { get; set; }
    }
}
