using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ISubscriptionModelInterface
    {
        PagedList<SubscriptionModel> GetSubscriptionModelsOverview(PageParamsRequestModel pageParamsRequestModel);
        Task<SubscriptionModel> GetSubscriptionModelById(int subscriptionModelId);
        Response CreateSubscriptionModel(SubscriptionModelCreateRequest subscriptionModelRequest);
        Response UpdateSubscriptionModel(SubscriptionModelUpdateRequest subscriptionModelRequest);
        bool DeleteSubscriptionModel(int subscriptionModelId);
    }
}

