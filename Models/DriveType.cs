using Nikitin.Views;
using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class DriveType
    {
        public DriveType()
        {
            Cars = new HashSet<Car>();
        }

        public int IdDriveType { get; set; }
        public string DriveTypeName { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return DriveTypeName;
        }
    }
}
