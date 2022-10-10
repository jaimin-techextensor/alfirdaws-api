using alfirdawsmanager.Data.Models;

namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class PricingModelCreateRequest
    {
        public int? SubscriptionModelId { get; set; }
        public int? PeriodTypeId { get; set; }
        public int? NrOfDays { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? Saving { get; set; }
        public decimal? PricePerDay { get; set; }
    }

    public class PricingModelUpdateRequest
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
    }
}

