using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseStatusReason
    {
        public CaseStatusReason()
        {
            Cases = new HashSet<Case>();
        }

        public int CaseStatusReasonId { get; set; }
        public int? CaseStatusId { get; set; }
        public string? Name { get; set; }
        public bool? Active { get; set; }

        public virtual CaseStatus? CaseStatus { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
