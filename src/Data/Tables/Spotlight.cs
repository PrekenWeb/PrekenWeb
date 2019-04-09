using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Data.Attributes;

namespace Data.Tables
{
    public class Spotlight
    {
        public Spotlight()
        {
            TaalId = 1;
            NieuwVenster = false;
            
        }

        public int Id { get; set; }  
        
        [DisplayName("Titel"), Required(ErrorMessage = "Titel is verplicht"), Tooltip("Vul hier de grote titel in, bijvoorbeeld een bijbeltekst")]
        public string Titel { get; set; }  
        
        [DisplayName("Sub Titel"), Required(ErrorMessage = "Subtitel is verplicht"), Tooltip("Vul hier de subtitel in, bijvoorbeeld een verwijzing naar het vers van de bijbeltekst")]
        public string Subtitel { get; set; }  
        
        [DisplayName("Link Titel"), Required(ErrorMessage = "Linktitel is verplicht"), Tooltip("De tekst van de link waarop kan worden geklikt, bijvoorbeeld: 'Luister de preek'")]
        public string LinkTitel { get; set; }  
       
        [DisplayName("Url"), DataType(DataType.Url), Required(ErrorMessage = "Url is verplicht"), Tooltip("Vul hier de gerelateerde URL in, dit kan een 'absolute' URL zijn, beginnend met 'https://www' of een relatieve beginnend met '~/' wat staat voor 'https://www.prekenweb.nl/'")]
        public string Url { get; set; } 
        
        [DisplayName("Afbeelding"), Required(ErrorMessage = "Afbeelding is verplicht"), UIHint("Afbeelding")]
        public int AfbeeldingId { get; set; }  
        
        [DisplayName("Sortering"), Range(0, 99999, ErrorMessage = "Voer een heel getal tussen de 0 en 99999 in!"), Required(ErrorMessage = "Sortering is verplicht"), Tooltip("Vul hier een sortering in, een heel getal tussen de 0 en 99999, hoe lager hoe eerder in beeld.")]
        public int Sortering { get; set; } 
       
        public int TaalId { get; set; }  
       
        public bool NieuwVenster { get; set; } 
         
        public virtual Afbeelding Afbeelding { get; set; } 
        public virtual Taal Taal { get; set; } 

   
        
    }

}
