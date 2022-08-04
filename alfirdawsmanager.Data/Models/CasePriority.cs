using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CasePriority
    {
        public CasePriority()
        {
            Cases = new HashSet<Case>();
        }

        public int CasePriorityId { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
