using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class CaseCategory
    {
        public CaseCategory()
        {
            Cases = new HashSet<Case>();
        }

        public int CaseCategoryId { get; set; }
        public string? Name { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Case> Cases { get; set; }
    }
}
