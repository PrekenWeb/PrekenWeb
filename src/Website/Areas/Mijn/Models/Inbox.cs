using System.Collections.Generic;
using System.ComponentModel;
using PrekenWeb.Data.Tables;
using Prekenweb.Models;

namespace Prekenweb.Website.Areas.Mijn.Models
{
    public class InboxIndexViewModel
    {
        public List<Inbox> InboxIitems { get; set; }
    }
    public class InboxEditViewModel
    {
        public Inbox InboxItem { get; set; }
    }
    public class InboxOpvolgingToevoegenViewModel
    {
        [DisplayName("Direct afhandelen")]
        public bool DirectAfhandelen { get; set; }

        public Inbox Inbox { get; set; }
        public InboxOpvolging InboxOpvolging { get; set; }
    }
}
