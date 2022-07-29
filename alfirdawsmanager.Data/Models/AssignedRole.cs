using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class AssignedRole
    {
        public int AssignedRoleId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual User? User { get; set; }
    }
}
