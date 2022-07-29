using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Module
    {
        public Module()
        {
            Permissions = new HashSet<Permission>();
        }

        public int ModuleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
