using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IPeriodTypeInterface
    {
        Task<List<PeriodTypeModel>> GetPeriodTypesOverview();
        Task<PeriodTypeModel> GetPeriodTypeById(int periodTypeId);
        Response CreatePeriodType(PeriodTypeCreateRequest periodTypeRequest);
        Response UpdatePeriodType(PeriodTypeUpdateRequest periodTypeRequest);
        bool DeletePeriodType(int periodTypeId);
    }
}

