using alfirdawsmanager.Data.Models;

namespace alfirdawsmanager.Service.Models
{
    public class PricingModel
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
        public List<PeriodTypeModel> PeriodTypes { get; set; }
        public string PeriodType { get; set; }
        public string SubscriptionModel { get; set; }
        public List<Models.SubscriptionModel> SubscriptionModels { get; set; }
    }
}

