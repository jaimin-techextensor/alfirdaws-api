using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class PeriodType
    {
        public PeriodType()
        {
            Campaigns = new HashSet<Campaign>();
            PricingModels = new HashSet<PricingModel>();
        }

        public int PeriodTypeId { get; set; }
        public string Name { get; set; }
        public int NrOfDays { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<PricingModel> PricingModels { get; set; }
    }
}
