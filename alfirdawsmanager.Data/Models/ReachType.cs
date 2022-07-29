using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class ReachType
    {
        public ReachType()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int ReachTypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
