using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class CarModel
    {
        public CarModel()
        {
            Cars = new HashSet<Car>();
        }

        public int IdCarModel { get; set; }
        public string ModelName { get; set; } = null!;
        public int IdBrand { get; set; }

        public virtual Brand IdBrandNavigation { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return $"{IdBrandNavigation.BrandName} {ModelName}";
        }
    }
}
