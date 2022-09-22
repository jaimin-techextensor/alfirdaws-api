using alfirdawsmanager.Data.Models;

namespace alfirdawsmanager.Service.Models
{
    public class CampaignModel
    {
        public int CampaignId { get; set; }
        public int CampaignTypeId { get; set; }
        public int ReachTypeId { get; set; }
        public int PeriodTypeId { get; set; }
        public string? Visual { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? Saving { get; set; }
        public decimal? PricePerDay { get; set; }
        public string? Description { get; set; }
        public string? ImpactPosition { get; set; }
        public string? ImpactViews { get; set; }
        public bool? Active { get; set; }
        public string  CampaignTypeName { get; set; }
        public string PeriodTypeName { get; set; }
        public string ReachTypeName { get; set; }
        public int NrOfDays { get; set; }
        public List<CampaignTypeModel> CampaignTypes { get; set; }
        public List<ReachTypeModel> ReachTypes { get; set; }
        public List<PeriodTypeModel> PeriodTypes { get; set; }
    }
}

