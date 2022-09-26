using System;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IVatTypeInterface
    {
        Task<List<VatTypeModel>> GetVATTypesOverview();
        Task<VatTypeModel> GetVATTypeById(int vatTypeId);
        Response CreateVATType(VatTypeCreateRequest vatTypeRequest);
        Response UpdateVATType(VatTypeUpdateRequest vatTypeRequest);
        bool DeleteVATType(int vatTypeId);
    }
}

