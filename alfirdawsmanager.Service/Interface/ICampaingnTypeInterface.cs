using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICampaignTypeInterface
    {
        Task<List<CampaignTypeModel>> GetCampaignTypesOverview();
        Task<CampaignTypeModel> GetCampaignTypeById(int categoryId);
        Response CreateCampaignType(CampaignTypeCreateRequest campaignTypeRequest);
        Response UpdateCampaignType(CampaignTypeUpdateRequest campaignTypeRequest);
        bool DeleteCampaignType(int campaignTypeId);
    }
}

