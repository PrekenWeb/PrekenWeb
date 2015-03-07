using System.Collections.Generic;

namespace Prekenweb.Models
{
    public class InboxType
    {
        public int Id { get; set; }  
        public string Omschrijving { get; set; } 

        public virtual ICollection<Inbox> Inboxes { get; set; }  

        public InboxType()
        {
            Inboxes = new List<Inbox>();
            
        }
        
    }

}
