using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class Car
    {
        public Car()
        {
            Requests = new HashSet<Request>();
        }

        public int IdCar { get; set; }
        public string StateNumber { get; set; } = null!;
        public int IdDriver { get; set; }
        public int IdCarStatus { get; set; }
        public int IdTransmission { get; set; }
        public int IdFuelType { get; set; }
        public int IdDriveType { get; set; }
        public int IdCarModel { get; set; }

        public virtual CarModel IdCarModelNavigation { get; set; } = null!;
        public virtual CarStatus IdCarStatusNavigation { get; set; } = null!;
        public virtual DriveType IdDriveTypeNavigation { get; set; } = null!;
        public virtual Employee IdDriverNavigation { get; set; } = null!;
        public virtual FuelType IdFuelTypeNavigation { get; set; } = null!;
        public virtual Transmission IdTransmissionNavigation { get; set; } = null!;
        public virtual ICollection<Request> Requests { get; set; }
    }
}
