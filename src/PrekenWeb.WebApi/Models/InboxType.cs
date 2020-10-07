using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class InboxType
    {
        public InboxType()
        {
            Inbox = new HashSet<Inbox>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<Inbox> Inbox { get; set; }
    }
}
