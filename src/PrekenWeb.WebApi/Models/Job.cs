using System;
using System.Collections.Generic;

namespace PrekenWeb.WebApi.Models
{
    public partial class Job
    {
        public Job()
        {
            JobParameter = new HashSet<JobParameter>();
            State = new HashSet<State>();
        }

        public int Id { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string InvocationData { get; set; }
        public string Arguments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }

        public virtual ICollection<JobParameter> JobParameter { get; set; }
        public virtual ICollection<State> State { get; set; }
    }
}
