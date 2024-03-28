using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class Request
    {
        public int IdRequest { get; set; }
        public int IdPurityStatus { get; set; }
        public int IdRequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal FirstPointLng { get; set; }
        public decimal FirstPointLat { get; set; }
        public decimal SecondPointLng { get; set; }
        public decimal SecondPintLat { get; set; }
        public int IdCar { get; set; }

        public virtual Car IdCarNavigation { get; set; } = null!;
        public virtual PurityStatus IdPurityStatusNavigation { get; set; } = null!;
        public virtual RequestStatus IdRequestStatusNavigation { get; set; } = null!;
    }
}
