using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseOrigin
    {
        public CaseOrigin()
        {
            Cases = new HashSet<Case>();
        }

        public int CaseOriginId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
