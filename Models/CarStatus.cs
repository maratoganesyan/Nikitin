using Nikitin.Views;
using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class CarStatus
    {
        public CarStatus()
        {
            Cars = new HashSet<Car>();
        }

        public int IdCarStatus { get; set; }
        public string CarStatusName { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return CarStatusName;
        }

    }
}
