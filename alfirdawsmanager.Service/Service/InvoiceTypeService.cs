using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class InvoiceTypeService : IInvoiceTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public InvoiceTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all invoice types
        /// </summary>
        /// <returns>List of invoice types</returns>
        public Task<List<InvoiceTypeModel>> GetInvoiceTypeOverview()
        {
            try
            {
                List<InvoiceTypeModel> invoiceTypes = _context.InvoiceTypes
                                                .Select(c => new InvoiceTypeModel
                                                {
                                                    Name = c.Name,
                                                    InvoiceTypeId = c.InvoiceTypeId
                                                }).ToList();

                return Task.FromResult(invoiceTypes);
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Creates a new invoice type
        /// </summary>
        /// <param name="invoiceTypeRequest">Invoice type request</param>
        /// <returns><Response object with true or false/returns>
        public Response CreateInvoiceType(InvoiceTypeCreateRequest invoiceTypeRequest)
        {
            try
            {
                Response response = new Response();

                var existInvoiceType = _context.InvoiceTypes.FirstOrDefault(a => a.Name.Equals(invoiceTypeRequest.Name));
                if (existInvoiceType == null)
                {
                    var objInvoicetype = new InvoiceType();
                    objInvoicetype.Name = invoiceTypeRequest.Name;
                    _context.Add(objInvoicetype);
                    _context.SaveChanges();
                    response.Data = objInvoicetype;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invoice Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a specific invoice type
        /// </summary>
        /// <param name="invoiceTypeId">The unique id of the invoice type</param>
        /// <returns>Boolean indication if the deletion was successfull or not</returns>
        public bool DeleteInvoiceType(int invoiceTypeId)
        {
            try
            {
                bool success = false;
                var objInvoiceType = _context.InvoiceTypes.FirstOrDefault(a => a.InvoiceTypeId == invoiceTypeId);
                if (objInvoiceType != null)
                {
                    _context.Remove(objInvoiceType);
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
        /// Retrieves an invoice type by its id
        /// </summary>
        /// <param name="invoiceTypeId">Unique id of the invoice type</param>
        /// <returns>Invoice Type object</returns>

        public Task<InvoiceTypeModel> GetInvoiceTypeById(int invoiceTypeId)
        {
            try
            {
                InvoiceTypeModel invoiceTypeModel = new InvoiceTypeModel();
                var invoiceType = _context.InvoiceTypes.FirstOrDefault(s => s.InvoiceTypeId == invoiceTypeId);
                if (invoiceType != null)
                {
                    invoiceTypeModel = _mapper.Map<InvoiceTypeModel>(invoiceType);
                }

                return Task.FromResult(invoiceTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates an invoice Type
        /// </summary>
        /// <param name="invoiceTypeRequest">The invoice type request</param>
        /// <returns>Response object with success or failure</returns>
        public Response UpdateInvoiceType(InvoiceTypeUpdateRequest invoiceTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existInvoiceType = _context.InvoiceTypes.FirstOrDefault(a => a.Name.Equals(invoiceTypeRequest.Name) && a.InvoiceTypeId != invoiceTypeRequest.InvoiceTypeId);
                if (existInvoiceType == null)
                {
                    var objInvoiceType = _context.InvoiceTypes.FirstOrDefault(a => a.InvoiceTypeId == invoiceTypeRequest.InvoiceTypeId);
                    if (objInvoiceType != null)
                    {
                        if (objInvoiceType.Name != null) objInvoiceType.Name = invoiceTypeRequest.Name;
                        _context.Update(objInvoiceType);
                        _context.SaveChanges();
                        response.Data = objInvoiceType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invoice Type with same name already exist.";
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

