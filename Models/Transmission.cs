using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class Transmission
    {
        public Transmission()
        {
            Cars = new HashSet<Car>();
        }

        public int IdTransmission { get; set; }
        public string TransmissionName { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return TransmissionName;
        }
    }
}
