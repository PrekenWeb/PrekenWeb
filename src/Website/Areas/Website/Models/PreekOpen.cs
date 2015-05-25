using PrekenWeb.Data.Tables;
using Prekenweb.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Prekenweb.Website.ViewModels
{
    public class PreekOpen
    {
        public Preek Preek { get; set; }
        public string Titel { get; set; }
        public PreekCookie Cookie { get; set; }

        [Display(Name = "LaatsteBezoek", ResourceType = typeof(Resources.Resources)), DisplayFormat(DataFormatString = "{0:g}")] 
        public DateTime? LaatsteBezoek { get; set; }
    }
}
