using Prekenweb.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Prekenweb.Website.ViewModels
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
