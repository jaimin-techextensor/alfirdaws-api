using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseStatus
    {
        public CaseStatus()
        {
            CaseStatusReasons = new HashSet<CaseStatusReason>();
            Cases = new HashSet<Case>();
        }

        public int CaseStatusId { get; set; }
        public string? Name { get; set; }
        public int? Sequence { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<CaseStatusReason> CaseStatusReasons { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
