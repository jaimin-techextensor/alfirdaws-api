using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IPricingModelInterface
    {
        PagedList<PricingModel> GetPricingModelsOverview(PageParamsRequestModel pageParamsRequestModel);
        Task<PricingModel> GetPricingModelById(int subscriptionModelId);
        Response CreatePricingModel(PricingModelCreateRequest subscriptionModelRequest);
        Response UpdatePricingModel(PricingModelUpdateRequest subscriptionModelRequest);
        bool DeletePricingModel(int subscriptionModelId);
    }
}

