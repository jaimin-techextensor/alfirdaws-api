namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class CampaignTypeCreateRequest
    {
        public string? Name { get; set; }
    }

    public class CampaignTypeUpdateRequest
    {
        public int? CampaignTypeId { get; set; }
        public string? Name { get; set; }
    }
}

