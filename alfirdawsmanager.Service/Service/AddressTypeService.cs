using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class AddressTypeService : IAddressTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public AddressTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the address types within the platform
        /// </summary>
        /// <returns>List of address types</returns>
        public Task<List<AddressTypeModel>> GetAddressTypesOverview()
        {
            try
            {
                List<AddressTypeModel> addressTypes= _context.AddressTypes
                                                .Select(c => new AddressTypeModel
                                                {
                                                    Name = c.Name,
                                                    AddressTypeId = c.AddressTypeId
                                                }).ToList();

                return Task.FromResult(addressTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new address type 
        /// </summary>
        /// <param name="addressTypeRequest">The address type request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateAddressType(AddressTypeCreateRequest addressTypeRequest)
        {
            try
            {
                Response response = new Response();

                var existAddressType = _context.AddressTypes.FirstOrDefault(a => a.Name.Equals(addressTypeRequest.Name));
                if (existAddressType == null)
                {
                    var objAddressType = new AddressType();
                    objAddressType.Name = addressTypeRequest.Name;
                    _context.Add(objAddressType);
                    _context.SaveChanges();
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Address Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates a address type
        /// </summary>
        /// <param name="addressTypeRequest">The address type update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateAddressType(AddressTypeUpdateRequest addressTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existAddressType = _context.AddressTypes.FirstOrDefault(a => a.Name.Equals(addressTypeRequest.Name) && a.AddressTypeId != addressTypeRequest.AddressTypeId);
                if (existAddressType == null)
                {
                    var objAddressType = _context.AddressTypes.FirstOrDefault(a => a.AddressTypeId == addressTypeRequest.AddressTypeId);
                    if (objAddressType != null)
                    {
                        if (objAddressType.Name != null) objAddressType.Name = addressTypeRequest.Name;
                        _context.Update(objAddressType);
                        _context.SaveChanges();
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Address Type with same name already exist.";
                }
                
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a address type
        /// </summary>
        /// <param name="addressTypeId">The unique id of the address type that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteAddressType(int addressTypeId)
        {
            try
            {
                bool success = false;
                var objAddressType = _context.AddressTypes.FirstOrDefault(a => a.AddressTypeId == addressTypeId);
                if (objAddressType != null)
                {
                    _context.Remove(objAddressType);
                    _context.SaveChanges();
                    success = true;
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific address type 
        /// </summary>
        /// <param name="addressTypeId">The unique id of the address type</param>
        /// <returns>The address type object</returns>
        public Task<AddressTypeModel> GetAddressTypeById(int addressTypeId)
        {
            try
            {
                AddressTypeModel adddressTypeModel = new AddressTypeModel();
                var addressType = _context.AddressTypes.FirstOrDefault(s => s.AddressTypeId == addressTypeId);
                if (addressType != null)
                {
                    adddressTypeModel = _mapper.Map<AddressTypeModel>(addressType);
                }

                return Task.FromResult(adddressTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

