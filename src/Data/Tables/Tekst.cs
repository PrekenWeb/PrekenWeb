using System.ComponentModel.DataAnnotations;

namespace Data.Tables
{
    public class Tekst
    {
        public int Id { get; set; } 
       
        [Required(AllowEmptyStrings = false), Display(Name = "Kop")]
        public string Kop { get; set; }  
     
        [DataType(DataType.Html), UIHint("Html"), Display(Name = "Tekst"), Required(AllowEmptyStrings = false)]
        public string Tekst_ { get; set; }  
     
        [Required, UIHint("Taal"), Display(Name = "Taal")]
        public int TaalId { get; set; } 
        
        public int? PaginaId { get; set; }  

        public virtual Pagina Pagina { get; set; }  
        
        public virtual Taal Taal { get; set; }  

        public Tekst()
        {
            Tekst_ = "1";
            
        }
        
    }

}
