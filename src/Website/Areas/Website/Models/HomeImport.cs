using System.ComponentModel;

namespace Prekenweb.Website.ViewModels
{
    public class HomeImport
    {
        [DisplayName("Zeker weten?")]
        public bool ZekerWeten { get; set; }
        [DisplayName("Docs (oude preken)")]
        public bool Docs { get; set; }
        [DisplayName("Cats (structuur)")]
        public bool Cats { get; set; }
    }
}
