using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            AssignedRoles = new HashSet<AssignedRole>();
            Permissions = new HashSet<Permission>();
        }

        public int RoleId { get; set; }
        public string? Name { get; set; }
        public bool? IsStatic { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<AssignedRole> AssignedRoles { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
