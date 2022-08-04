using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseType
    {
        public CaseType()
        {
            Cases = new HashSet<Case>();
        }

        public int CaseTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
