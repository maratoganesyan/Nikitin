using Nikitin.Views;
using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class FuelType
    {
        public FuelType()
        {
            Cars = new HashSet<Car>();
        }

        public int IdFuelType { get; set; }
        public string FuelTypeName { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return FuelTypeName;
        }
    }
}
