namespace alfirdawsmanager.Service.Models
{
    public class SubscriptionModel
    {
        public int SubscriptionModelId { get; set; }
        public string Name { get; set; }
        public string? Visual { get; set; }
        public string UserType { get; set; }
        public string SubscriptionType { get; set; }
        public int? NrOfAds { get; set; }
        public int? NrOfPictures { get; set; }
        public bool? UnlimitedAds { get; set; }
        public bool? UnlimitedPictures { get; set; }
        public bool? IsSearchEngine { get; set; }
        public bool? IsVouchers { get; set; }
        public bool? IsSocialMedia { get; set; }
        public bool? IsBasicCampaigns { get; set; }
        public bool? IsExtendedCampaigns { get; set; }
        public bool? IsStatistics { get; set; }
        public bool? IsTrends { get; set; }
        public bool? IsPartnership { get; set; }
        public bool? IsOnlineSupport { get; set; }
        public bool? Active { get; set; }
        public string? Description { get; set; }
    }
}

