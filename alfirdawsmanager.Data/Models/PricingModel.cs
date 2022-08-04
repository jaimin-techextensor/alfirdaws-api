using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class PricingModel
    {
        public int PricingModelId { get; set; }
        public int? SubscriptionModelId { get; set; }
        public int? PeriodTypeId { get; set; }
        public int? NrOfDays { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? Saving { get; set; }
        public decimal? PricePerDay { get; set; }

        public virtual PeriodType? PeriodType { get; set; }
        public virtual SubscriptionModel? SubscriptionModel { get; set; }
    }
}
