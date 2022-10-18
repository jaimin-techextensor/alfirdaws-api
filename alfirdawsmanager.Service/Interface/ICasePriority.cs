using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICasePriorityInterface
    {
        Task<List<CasePriorityModel>> GetCasePriorityOverview();
        Task<CasePriorityModel> GetCasePriorityById(int casePriorityId);
        Response CreateCasePriority(CasePriorityCreateRequest casePriorityRequest);
        Response UpdateCasePriority(CasePriorityUpdateRequest casePriorityRequest);
        bool DeleteCasePriority(int casePriorityId);
    }
}
