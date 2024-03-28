using Nikitin.Views;
using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class Brand
    {
        public Brand()
        {
            CarModels = new HashSet<CarModel>();
        }

        public int IdBrand { get; set; }
        public string BrandName { get; set; } = null!;

        public virtual ICollection<CarModel> CarModels { get; set; }

        public override string ToString()
        {
            return BrandName;
        }
    }
}
