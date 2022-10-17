using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICaseCategoryInterface
    {
        Task<List<CaseCategoryModel>> GetCaseCategoryOverview();
        Task<CaseCategoryModel> GetCaseCategoryById(int caseCategoryId);
        Response CreateCaseCategory(CaseCategoryCreateRequest caseCategoryRequest);
        Response UpdateCaseCategory(CaseCategoryUpdateRequest caseCategoryRequest);
        bool DeleteCaseCategory(int caseCategoryId);

    }
}
