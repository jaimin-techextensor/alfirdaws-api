using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICampaignInterface
    {
        PagedList<CampaignModel> GetCampaignsOverview(PageParamsRequestModel pageParamsRequestModel);
        Task<CampaignModel> GetCampaignById(int campaignId);
        Response CreateCampaign(CampaignCreateRequest campaignRequest);
        Response UpdateCampaign(CampaignUpdateRequest campaignRequest);
        bool DeleteCampaign(int campaignId);
    }
}

