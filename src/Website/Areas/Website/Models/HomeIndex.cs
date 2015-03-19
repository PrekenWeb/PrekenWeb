using Prekenweb.Models;
using Prekenweb.Models.Services;
using Prekenweb.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prekenweb.Website.ViewModels
{
    public class HomeIndex
    {
        public InschrijvenNieuwsbrief InschrijvenNieuwsbriefForm { get; set; }
        public IList<Tekst> Teksten { get; set; }
        public IList<Spotlight> SpotlightItems { get; set; }
        public TekstPagina WelkomsTekst { get; set; }
        public string Taal { get; set; }
        public NieuwePreken NieuwePreken { get; set; } 
    }

    public class NieuwePreken
    {
        public bool LeesPreken { get; set; }
        public bool AudioPreken { get; set; }
        public bool Lezingen { get; set; }
        public IList<ZoekresultaatItem> Preken { get; set; }
        
        public NieuwePreken()
        {
            LeesPreken = true;
            AudioPreken = true;
            Lezingen = true;
        }

        public NieuwePreken(bool leesPreken, bool audioPreken, bool lezingen)
        {
            LeesPreken = leesPreken;
            AudioPreken = audioPreken;
            Lezingen = lezingen;
        }

        public static string GetKopLabel(bool leesPreken, bool audioPreken, bool lezingen)
        {
            string kop = Resources.Resources.NieuwePreken;
            if (!(leesPreken && lezingen && audioPreken))
            {
                if (audioPreken && leesPreken) { kop = Resources.Resources.LeesEnAudioPreken; }
                else if (audioPreken && lezingen) { kop = Resources.Resources.LezingenEnAudioPreken; }
                else if (lezingen && leesPreken) { kop = Resources.Resources.LeesprekenEnLezingen; }
                else if (leesPreken) { kop = Resources.Resources.NieuweLeespreken; }
                else if (lezingen) { kop = Resources.Resources.NieuweLezingen; }
                else if (audioPreken) { kop = Resources.Resources.NieuweAudioPreken; }
            }
            return kop;
        }
    }

    public class InschrijvenNieuwsbrief
    {
        [Required(ErrorMessageResourceName = "NieuwsbriefNaamVerplicht", ErrorMessageResourceType = typeof(Resources.Resources)), MinLength(5, ErrorMessage="Min. 5 characters")]
        [Display(Name = "NiewsbriefNaam", ResourceType = typeof(Resources.Resources))]
        public string Naam { get; set; }

        [Required(ErrorMessageResourceName = "NieuwsbriefEmailVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "NieuwsbriefEmail", ResourceType = typeof(Resources.Resources)), EmailAddress(ErrorMessage = "Geen email adres")]
        public string Email { get; set; }
    }
}
