using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Tables;
using Data.ViewModels;

namespace Prekenweb.Website.Areas.Website.Models
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
        public bool VideoPreken { get; set; }
        public bool Lezingen { get; set; }
        public IList<ZoekresultaatItem> Preken { get; set; }
        
        public NieuwePreken()
        {
            LeesPreken = true;
            AudioPreken = true;
            VideoPreken = true;
            Lezingen = true;
        }

        public static string GetKopLabel(bool leesPreken, bool audioPreken, bool lezingen, bool videoPreken)
        {
            if (leesPreken && audioPreken && lezingen && videoPreken)
                return Resources.Resources.NieuwePreken;
            else if ((audioPreken || videoPreken) && leesPreken)
                return videoPreken ? Resources.Resources.NieuweLeesEnAudioVideoPreken : Resources.Resources.NieuweLeesEnAudioPreken;
            else if ((audioPreken || videoPreken) && lezingen)
                return videoPreken? Resources.Resources.NieuweLezingenEnAudioVideoPreken : Resources.Resources.NieuweLezingenEnAudioPreken;
            else if (lezingen && leesPreken)
                return Resources.Resources.NieuweLeesprekenEnLezingen;
            else if (leesPreken)
                return Resources.Resources.NieuweLeespreken;
            else if (lezingen)
                return Resources.Resources.NieuweLezingen;
            else if (audioPreken && !videoPreken)
                return Resources.Resources.NieuweAudioPreken;
            else if (videoPreken)
                return Resources.Resources.NieuweVideoPreken;
            else
                return Resources.Resources.NieuwePreken;
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
