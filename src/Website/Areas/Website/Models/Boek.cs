using Prekenweb.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Prekenweb.Website.ViewModels
{
    public class BoekViewModel
    {
        public TekstPagina TekstPagina { get; set; }

        [Display(Name = "Aanhef"), Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public AanhefEnum Aanhef { get; set; }

        [Display(Name = "Naam", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NaamIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Naam { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Dit is geen geldig e-mail adres!")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "EmailIsVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Display(Name = "Postcode"), Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Postcode { get; set; }

        [Display(Name = "Woonplaats"), Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Woonplaats { get; set; }

        [Display(Name = "Straat"), Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Straat { get; set; }

        [Display(Name = "Huisnummer"), Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Huisnummer { get; set; }

        [Display(Name = "Boek"), Required(ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public int BoekId { get; set; }

        [Display(Name = "Aantal"), Required(ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources)), Range(1, 20)]
        public int Aantal { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Opmerking", ResourceType = typeof(Resources.Resources))]
        public string Tekst { get; set; }

        [Display(Name = "Verzendmethode"), Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VeldVerplicht", ErrorMessageResourceType = typeof(Resources.Resources))]
        public VerzendMethodes VerzendMethode { get; set; }

        [Display(Name = "Ophalen in")]
        public string OphaalLocatie { get; set; }
         
        public string Prijs { get; set; }

        public IEnumerable<SelectListItem> Boeken
        {
            get
            {
                return new List<SelectListItem>(){
                    //new SelectListItem(){ Value= "", Text= "", Selected = true },
                    new SelectListItem(){ Value= "1", Text= "Jona de profeet", Selected = true },
                    new SelectListItem(){ Value= "2", Text= "Die dorst heeft, kome"} 
                };
            }
        }

        public enum AanhefEnum
        {
            Dhr,
            Mevr
        }
        public enum VerzendMethodes
        {
            Ophalen,
            Verzenden
        }
        public IEnumerable<SelectListItem> OphaalLocaties
        {
            get
            {
                return new List<SelectListItem>(){
                    new SelectListItem(){ Value= "", Text= "", Selected = true },
                    new SelectListItem(){ Value= "Aagtekerke", Text= "Aagtekerke"},
                    new SelectListItem(){ Value= "Hendrik-Ido-Ambacht", Text= "Hendrik-Ido-Ambacht"} ,
                    new SelectListItem(){ Value= "Kapelle (Zeeland)", Text= "Kapelle (Zeeland)"} ,
                    new SelectListItem(){ Value= "Lisse", Text= "Lisse"} ,
                };
            }
        }
        public bool Verzonden { get; set; }

    }


}
