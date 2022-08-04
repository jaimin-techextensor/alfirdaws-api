using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseSeverity
    {
        public CaseSeverity()
        {
            Cases = new HashSet<Case>();
        }

        public int CaseSeverityId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
