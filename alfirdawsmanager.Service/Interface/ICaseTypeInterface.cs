using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICaseTypeInterface
    {
        Task<List<CaseTypeModel>> GetCaseTypesOverview();
        Task<CaseTypeModel> GetCaseTypeById(int caseTypeId);
        Response CreateCaseType(CaseTypeCreateRequest caseTypeRequest);
        Response UpdateCaseType(CaseTypeUpdateRequest caseTypeRequest);
        bool DeleteCaseType(int caseTypeId);
    }
}

