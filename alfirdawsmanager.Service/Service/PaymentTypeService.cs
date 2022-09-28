using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class PaymentTypeService : IPaymentTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public PaymentTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Returns the list of all available paypment types
        /// </summary>
        /// <returns>List of payment types</returns>
        public Task<List<PaymentTypeModel>> GetPaymentTypeOverview()
        {
            try
            {
                List<PaymentTypeModel> paymentTypes = _context.PaymentTypes
                                                .Select(c => new PaymentTypeModel
                                                {
                                                    Name = c.Name,
                                                    PaymentTypeId = c.PaymentTypeId,
                                                    Icon = c.Icon
                                                }).ToList();

                return Task.FromResult(paymentTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Creates a new payment type 
        /// </summary>
        /// <param name="paymentTypeRequest">The payment type request</param>
        /// <returns>Respones object with success or failure message</returns>
        public Response CreatePaymentType(PaymentTypeCreateRequest paymentTypeRequest)
        {
            try
            {
                Response response = new Response();

                var existPaymentType = _context.PaymentTypes.FirstOrDefault(a => a.Name.Equals(paymentTypeRequest.Name));
                if (existPaymentType == null)
                {
                    var objPaymentType = new PaymentType();
                    objPaymentType.Name = paymentTypeRequest.Name;
                    objPaymentType.Icon = paymentTypeRequest.Icon;
                    _context.Add(objPaymentType);
                    _context.SaveChanges();
                    response.Data = objPaymentType;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Payment type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Removes a payment type
        /// </summary>
        /// <param name="paymentTypeId">The unique id of the payment type</param>
        /// <returns>Boolean with success indication</returns>
        public bool DeletePaymentType(int paymentTypeId)
        {
            try
            {
                bool success = false;
                var objPaymentType = _context.PaymentTypes.FirstOrDefault(a => a.PaymentTypeId == paymentTypeId);
                if (objPaymentType != null)
                {
                    _context.Remove(objPaymentType);
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
        /// Retrieves a specific payment type
        /// </summary>
        /// <param name="paymentTypeId">The unique id of the payment type</param>
        /// <returns>Payment type object</returns>
        public Task<PaymentTypeModel> GetPaymentTypeById(int paymentTypeId)
        {
            try
            {
                PaymentTypeModel paymentTypeModel = null;
                var paymentType = _context.PaymentTypes.FirstOrDefault(s => s.PaymentTypeId == paymentTypeId);
                if (paymentType != null)
                {
                    paymentTypeModel = _mapper.Map<PaymentTypeModel>(paymentType);
                }

                return Task.FromResult(paymentTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a specific payment type
        /// </summary>
        /// <param name="paymentTypeRequest">The payment type request object</param>
        /// <returns>Response with success or failure</returns>
        public Response UpdatePaymentType(PaymentTypeUpdateRequest paymentTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existPaymentType = _context.PaymentTypes.FirstOrDefault(a => a.Name.Equals(paymentTypeRequest.Name) && a.PaymentTypeId != paymentTypeRequest.PaymentTypeId);
                if (existPaymentType == null)
                {
                    var objPaymentType = _context.PaymentTypes.FirstOrDefault(a => a.PaymentTypeId == paymentTypeRequest.PaymentTypeId);
                    if (objPaymentType != null)
                    {
                        if (objPaymentType.Name != null) objPaymentType.Name = paymentTypeRequest.Name;
                        if (objPaymentType.Icon != null) objPaymentType.Icon = paymentTypeRequest.Icon;
                        _context.Update(objPaymentType);
                        _context.SaveChanges();
                        response.Data = objPaymentType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Payment type with same name already exist.";
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

