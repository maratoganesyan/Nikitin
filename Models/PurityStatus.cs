using Nikitin.Views;
using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class PurityStatus
    {
        public PurityStatus()
        {
            Requests = new HashSet<Request>();
        }

        public int IdPurityStatus { get; set; }
        public string PurityStatusName { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }

        public override string ToString()
        {
            return PurityStatusName;
        }

    }
}
