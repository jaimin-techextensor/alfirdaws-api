using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Campaign
    {
        public int CampaignId { get; set; }
        public int? CampaignTypeId { get; set; }
        public int? ReachTypeId { get; set; }
        public int? PeriodTypeId { get; set; }
        public byte[]? Visual { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? Saving { get; set; }
        public decimal? PricePerDay { get; set; }
        public string? Description { get; set; }
        public string? ImpactPosition { get; set; }
        public string? ImpactViews { get; set; }
        public bool? Active { get; set; }

        public virtual CampaignType? CampaignType { get; set; }
        public virtual PeriodType? PeriodType { get; set; }
        public virtual ReachType? ReachType { get; set; }
    }
}
