using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class PricingModelService : IPricingModelInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public PricingModelService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the pricing models within the platform
        /// </summary>
        /// <returns>List of pricing models</returns>
        public PagedList<Models.PricingModel> GetPricingModelsOverview(PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(pageParamsRequestModel.SearchText) && pageParamsRequestModel.SearchText != "null")
                {
                    var dataToReturn = PagedList<Models.PricingModel>.ToPagedList(_context.PricingModels
                                                  .Where(a => (((a.SubscriptionModel.Name != null && a.SubscriptionModel.Name != null) && (a.SubscriptionModel.Name.Contains(pageParamsRequestModel.SearchText)))
                                                                          || ((a.PeriodType.Name != null && a.PeriodType.Name != null) && (a.PeriodType.Name.Contains(pageParamsRequestModel.SearchText)))))
                                                  .Select(c => new Models.PricingModel
                                                  {
                                                      SubscriptionModelId = c.SubscriptionModelId,
                                                      PeriodTypeId = c.PeriodTypeId,
                                                      NetPrice = c.NetPrice,
                                                      DiscountPercentage = c.DiscountPercentage,
                                                      NrOfDays = c.NrOfDays,
                                                      Price = c.Price,
                                                      PricePerDay = c.PricePerDay,
                                                      PricingModelId = c.PricingModelId,
                                                      Saving = c.Saving,
                                                      PeriodType = c.PeriodType.Name,
                                                      SubscriptionModel = c.SubscriptionModel.Name
                                                  }).AsQueryable(), pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);

                    return dataToReturn;
                }
                else
                {
                    var dataToReturn = PagedList<Models.PricingModel>.ToPagedList(_context.PricingModels.OrderByDescending(a => a.PricingModelId).Select(c => new Models.PricingModel
                    {
                        SubscriptionModelId = c.SubscriptionModelId,
                        PeriodTypeId = c.PeriodTypeId,
                        NetPrice = c.NetPrice,
                        DiscountPercentage = c.DiscountPercentage,
                        NrOfDays = c.NrOfDays,
                        Price = c.Price,
                        PricePerDay = c.PricePerDay,
                        PricingModelId = c.PricingModelId,
                        Saving = c.Saving,
                        PeriodType = c.PeriodType.Name,
                        SubscriptionModel = c.SubscriptionModel.Name
                    }).AsQueryable(), pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);
                    return dataToReturn;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new pricing model 
        /// </summary>
        /// <param name="pricingModelRequest">The pricing model request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreatePricingModel(PricingModelCreateRequest pricingModelRequest)
        {
            try
            {
                Response response = new Response();

                var objPricingModel = new Data.Models.PricingModel();
                objPricingModel.NetPrice = pricingModelRequest.NetPrice;
                objPricingModel.PricePerDay = pricingModelRequest.PricePerDay;
                objPricingModel.Price = pricingModelRequest.Price;
                objPricingModel.DiscountPercentage = pricingModelRequest.DiscountPercentage;
                objPricingModel.NrOfDays = pricingModelRequest.NrOfDays;
                objPricingModel.PeriodTypeId = pricingModelRequest.PeriodTypeId;
                objPricingModel.SubscriptionModelId = pricingModelRequest.SubscriptionModelId;
                objPricingModel.Saving = pricingModelRequest.Saving;

                _context.Add(objPricingModel);
                _context.SaveChanges();
                response.Success = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates a pricing model
        /// </summary>
        /// <param name="pricingModelRequest">The pricing model update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdatePricingModel(PricingModelUpdateRequest pricingModelRequest)
        {
            try
            {
                Response response = new Response();
                var objPricingModel = _context.PricingModels.FirstOrDefault(a => a.PricingModelId == pricingModelRequest.PricingModelId);
                if (objPricingModel != null)
                {
                    if (objPricingModel.DiscountPercentage != null) objPricingModel.DiscountPercentage = pricingModelRequest.DiscountPercentage;
                    if (objPricingModel.NrOfDays != null) objPricingModel.NrOfDays = pricingModelRequest.NrOfDays;
                    if (objPricingModel.Saving != null) objPricingModel.Saving = pricingModelRequest.Saving;
                    if (objPricingModel.PricePerDay != null) objPricingModel.PricePerDay = pricingModelRequest.PricePerDay;
                    if (objPricingModel.NetPrice != null) objPricingModel.NetPrice = pricingModelRequest.NetPrice;
                    if (objPricingModel.PeriodTypeId != null) objPricingModel.PeriodTypeId = pricingModelRequest.PeriodTypeId;
                    if (objPricingModel.SubscriptionModelId != null) objPricingModel.SubscriptionModelId = pricingModelRequest.SubscriptionModelId;
                    if (objPricingModel.Price != null) objPricingModel.Price = pricingModelRequest.Price;

                    _context.Update(objPricingModel);
                    _context.SaveChanges();
                    response.Success = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a pricing model
        /// </summary>
        /// <param name="pricingModelId">The unique id of the pricing model that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeletePricingModel(int pricingModelId)
        {
            try
            {
                bool success = false;
                var objPricingModel = _context.PricingModels.FirstOrDefault(a => a.PricingModelId == pricingModelId);
                if (objPricingModel != null)
                {
                    _context.Remove(objPricingModel);
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
        /// Retrieves a specific pricing model 
        /// </summary>
        /// <param name="pricingModelId">The unique id of the pricing model</param>
        /// <returns>The pricing model object</returns>
        public Task<Models.PricingModel> GetPricingModelById(int pricingModelId)
        {
            try
            {
                Models.PricingModel pricingModelObj = new Models.PricingModel();
                var pricingModel = _context.PricingModels.FirstOrDefault(s => s.PricingModelId == pricingModelId);
                if (pricingModelObj != null)
                {
                    pricingModelObj = _mapper.Map<Models.PricingModel>(pricingModel);
                    var subscriptionModels = _context.SubscriptionModels;
                    if (subscriptionModels != null)
                    {
                        pricingModelObj.SubscriptionModels = _mapper.Map<List<Models.SubscriptionModel>>(subscriptionModels.ToList().Select(a => new Models.SubscriptionModel
                        {
                            Name = a.Name,
                            SubscriptionModelId = a.SubscriptionModelId
                        }));
                    }
                    var periodTypes = _context.PeriodTypes;
                    if (periodTypes != null)
                    {
                        pricingModelObj.PeriodTypes = _mapper.Map<List<PeriodTypeModel>>(periodTypes.ToList().Select(a => new PeriodTypeModel
                        {
                            Name = a.Name,
                            PeriodTypeId = a.PeriodTypeId
                        }));
                    }
                }
                return Task.FromResult(pricingModelObj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

