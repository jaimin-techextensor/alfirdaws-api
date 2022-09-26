using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IAddressTypeInterface
    {
        Task<List<AddressTypeModel>> GetAddressTypesOverview();
        Task<AddressTypeModel> GetAddressTypeById(int addressTypeId);
        Response CreateAddressType(AddressTypeCreateRequest addressTypeRequest);
        Response UpdateAddressType(AddressTypeUpdateRequest addressTypeRequest);
        bool DeleteAddressType(int addressTypeId);
    }
}

