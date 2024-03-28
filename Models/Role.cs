using System;
using System.Collections.Generic;

namespace Nikitin.Models
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdRole { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            return RoleName;
        }
    }
}
