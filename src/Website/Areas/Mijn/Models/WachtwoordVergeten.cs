﻿using Prekenweb.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class WachtwoordVergeten
    {
        [Display(Name="Gebruikersnaam"), Required(AllowEmptyStrings=false)]
        public string Gebruikersnaam { get; set; }

        public TekstPagina TekstPagina { get; set; }
    }
}
