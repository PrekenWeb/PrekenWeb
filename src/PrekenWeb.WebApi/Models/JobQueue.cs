using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class JobQueue
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Queue { get; set; }
        public DateTime? FetchedAt { get; set; }
    }
}
