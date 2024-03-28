using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Cars = new HashSet<Car>();
        }

        public int IdEmployee { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }

        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic} {PhoneNumber} {Login} {Password} {IdRoleNavigation.RoleName}";
        }
    }
}
