using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            Requests = new HashSet<Request>();
        }

        public int IdRequestStatus { get; set; }
        public string RequestStatusName { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }

        public override string ToString()
        {
            return RequestStatusName;
        }
    }
}
