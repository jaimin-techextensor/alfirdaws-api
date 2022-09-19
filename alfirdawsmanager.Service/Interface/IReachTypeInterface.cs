using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IReachTypeInterface
    {
        Task<List<ReachTypeModel>> GetReachTypesOverview();
        Task<ReachTypeModel> GetReachTypeById(int reachTypeId);
        Response CreateReachType(ReachTypeCreateRequest reachTypeRequest);
        Response UpdateReachType(ReachTypeUpdateRequest reachTypeRequest);
        bool DeleteReachType(int reachTypeId);
    }
}

