using System;
using System.Collections.Generic;
using PrekenWeb.Data.ViewModels;

namespace Prekenweb.Website.Areas.Website.Models
{
    public class Nieuwsbrief
    {
        public TekstPagina TekstPagina { get; set; } 
        public List<MailChimpNieuwsbrief> Nieuwsbrieven { get; set; }

    }
    public class MailChimpNieuwsbrief
    {
        public DateTime Datum { get; set; }
        public string Titel { get; set; }
        public Uri ArchiveUrl { get; set; }
    }
}
