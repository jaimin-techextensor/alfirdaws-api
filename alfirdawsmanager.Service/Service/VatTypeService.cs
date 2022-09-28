using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class VatTypeService : IVatTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public VatTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the overview of all VAT types
        /// </summary>
        /// <returns>List of VAT type objects</returns>
        public Task<List<VatTypeModel>> GetVATTypesOverview()
        {
            try
            {
                List<VatTypeModel> vattypes = _context.Vattypes
                                                .Select(c => new VatTypeModel
                                                {
                                                    Name = c.Name,
                                                    VatTypeId = c.VattypeId,
                                                    Percentage = c.Percentage,
                                                    Description = c.Description
                                                }).ToList();

                return Task.FromResult(vattypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new VAT type
        /// </summary>
        /// <param name="vatTypeRequest">The VAT type request</param>
        /// <returns>Response object with success or failure</returns>
        public Response CreateVATType(VatTypeCreateRequest vatTypeRequest)
        {
            try
            {
                Response response = new Response();

                var existVATType = _context.Vattypes.FirstOrDefault(a => a.Name.Equals(vatTypeRequest.Name));
                if (existVATType == null)
                {
                    var objVATType = new Vattype();
                    objVATType.Name = vatTypeRequest.Name;
                    objVATType.Percentage = vatTypeRequest.Percentage;
                    objVATType.Description = vatTypeRequest.Description;

                    _context.Add(objVATType);
                    _context.SaveChanges();
                    response.Data = objVATType;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "VAT Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Repmoves a specific VAT type object
        /// </summary>
        /// <param name="vatTypeId">The unique id of the VAT type object</param>
        /// <returns>Boolean indication of the success or failure of the deletion</returns>
        public bool DeleteVATType(int vatTypeId)
        {
            try
            {
                bool success = false;
                var objVATType = _context.Vattypes.FirstOrDefault(a => a.VattypeId == vatTypeId);
                if (objVATType != null)
                {
                    _context.Remove(objVATType);
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
        /// Retrieves a specific VAT Type
        /// </summary>
        /// <param name="vatTypeId">Unique id of the VAT type</param>
        /// <returns>VAT type object</returns>
        public Task<VatTypeModel> GetVATTypeById(int vatTypeId)
        {
            try
            {
                VatTypeModel vatTypeModel = new VatTypeModel();
                var vatType = _context.Vattypes.FirstOrDefault(s => s.VattypeId == vatTypeId);
                if (vatType != null)
                {
                    vatTypeModel = _mapper.Map<VatTypeModel>(vatType);
                }

                return Task.FromResult(vatTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a specific VAT Type
        /// </summary>
        /// <param name="vatTypeRequest">VAT type request</param>
        /// <returns>Response object with the success or failure of the update</returns>
        public Response UpdateVATType(VatTypeUpdateRequest vatTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existVATType = _context.Vattypes.FirstOrDefault(a => a.Name.Equals(vatTypeRequest.Name) && a.VattypeId != vatTypeRequest.VatTypeId);
                if (existVATType == null)
                {
                    var objVATType = _context.Vattypes.FirstOrDefault(a => a.VattypeId == vatTypeRequest.VatTypeId);
                    if (objVATType != null)
                    {
                        if (objVATType.Name != null) objVATType.Name = vatTypeRequest.Name;
                        if (objVATType.Percentage != 0) objVATType.Percentage = vatTypeRequest.Percentage;
                        if (objVATType.Description != null) objVATType.Description = vatTypeRequest.Description;

                        _context.Update(objVATType);
                        _context.SaveChanges();
                        response.Data = objVATType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "VAT Type with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

