using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace alfirdawsmanager.Service.Service
{
    public class CampaignService : ICampaignInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public CampaignService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the campaigns within the platform
        /// </summary>
        /// <returns>List of campaigns</returns>
        public PagedList<CampaignModel> GetCampaignsOverview(PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(pageParamsRequestModel.SearchText) && pageParamsRequestModel.SearchText != "null")
                {
                    var dataToReturn = PagedList<CampaignModel>.ToPagedList(_context.Campaigns
                                                  .Include(a => a.CampaignType)
                                                  .Include(a => a.ReachType)
                                                  .Include(a => a.PeriodType)
                                                  .Where(a => (((a.CampaignType != null && a.CampaignType.Name != null) && (a.CampaignType.Name.Contains(pageParamsRequestModel.SearchText)))
                                                                          || ((a.ReachType != null && a.ReachType.Name != null) && (a.ReachType.Name.Contains(pageParamsRequestModel.SearchText)))
                                                                          || ((a.PeriodType != null && a.PeriodType.Name != null) && (a.PeriodType.Name.Contains(pageParamsRequestModel.SearchText)))))
                                                  .Select(c => new CampaignModel
                                                  {
                                                      CampaignId = c.CampaignId,
                                                      CampaignTypeId = c.CampaignTypeId,
                                                      ReachTypeId = c.ReachTypeId,
                                                      PeriodTypeId = c.PeriodTypeId,
                                                      CampaignTypeName = c.CampaignType.Name,
                                                      ReachTypeName = c.ReachType.Name,
                                                      PeriodTypeName = c.PeriodType.Name,
                                                      Description = c.Description,
                                                      Active = c.Active,
                                                      DiscountPercentage = c.DiscountPercentage,
                                                      ImpactPosition = c.ImpactPosition,
                                                      ImpactViews = c.ImpactViews,
                                                      NetPrice = c.NetPrice,
                                                      Price = c.Price,
                                                      PricePerDay = c.PricePerDay,
                                                      Saving = c.Saving,
                                                      Visual = c.Visual,
                                                      NrOfDays = c.PeriodType.NrOfDays
                                                  }).AsQueryable(), pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);

                    return dataToReturn;
                }
                else
                {
                    var dataToReturn = PagedList<CampaignModel>.ToPagedList(_context.Campaigns.OrderByDescending(a => a.CampaignId).Select(c => new CampaignModel
                    {
                        CampaignId = c.CampaignId,
                        CampaignTypeId = c.CampaignTypeId,
                        ReachTypeId = c.ReachTypeId,
                        PeriodTypeId = c.PeriodTypeId,
                        CampaignTypeName = c.CampaignType.Name,
                        ReachTypeName = c.ReachType.Name,
                        PeriodTypeName = c.PeriodType.Name,
                        Description = c.Description,
                        Active = c.Active,
                        DiscountPercentage = c.DiscountPercentage,
                        ImpactPosition = c.ImpactPosition,
                        ImpactViews = c.ImpactViews,
                        NetPrice = c.NetPrice,
                        Price = c.Price,
                        PricePerDay = c.PricePerDay,
                        Saving = c.Saving,
                        Visual = c.Visual,
                        NrOfDays = c.PeriodType.NrOfDays
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
        /// Create a new campaign 
        /// </summary>
        /// <param name="campaignRequest">The campaign request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateCampaign(CampaignCreateRequest campaignRequest)
        {
            try
            {
                Response response = new Response();

                var objCampaign = new Campaign();
                objCampaign.CampaignTypeId = campaignRequest.CampaignTypeId;
                objCampaign.PeriodTypeId = campaignRequest.PeriodTypeId;
                objCampaign.ReachTypeId = campaignRequest.ReachTypeId;
                objCampaign.Visual = campaignRequest.Visual;
                objCampaign.ImpactViews = campaignRequest.ImpactViews;
                objCampaign.Price = campaignRequest.Price;
                objCampaign.NetPrice = campaignRequest.NetPrice;
                objCampaign.Saving = campaignRequest.Saving;
                objCampaign.PricePerDay = campaignRequest.PricePerDay;
                objCampaign.DiscountPercentage = campaignRequest.DiscountPercentage;
                objCampaign.Active = campaignRequest.Active;
                objCampaign.Description = campaignRequest.Description;
                objCampaign.ImpactPosition = campaignRequest.ImpactPosition;

                _context.Add(objCampaign);
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
        /// Updates a campaign
        /// </summary>
        /// <param name="campaignRequest">The campaign update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateCampaign(CampaignUpdateRequest campaignRequest)
        {
            try
            {
                Response response = new Response();
                var objCampaign = _context.Campaigns.FirstOrDefault(a => a.CampaignId == campaignRequest.CampaignId);
                if (objCampaign != null)
                {
                    if (objCampaign.Description != null) objCampaign.Description = campaignRequest.Description;
                    if (objCampaign.Visual != null) objCampaign.Visual = campaignRequest.Visual;
                    if (objCampaign.Price != null) objCampaign.Price = campaignRequest.Price;
                    if (objCampaign.CampaignTypeId != null) objCampaign.CampaignTypeId = campaignRequest.CampaignTypeId;
                    if (objCampaign.ReachTypeId != null) objCampaign.ReachTypeId = campaignRequest.ReachTypeId;
                    if (objCampaign.PeriodTypeId != null) objCampaign.PeriodTypeId = campaignRequest.PeriodTypeId;
                    if (objCampaign.ImpactViews != null) objCampaign.ImpactViews = campaignRequest.ImpactViews;
                    if (objCampaign.ImpactPosition != null) objCampaign.ImpactPosition = campaignRequest.ImpactPosition;
                    if (objCampaign.NetPrice != null) objCampaign.NetPrice = campaignRequest.NetPrice;
                    if (objCampaign.Saving != null) objCampaign.Saving = campaignRequest.Saving;
                    if (objCampaign.DiscountPercentage != null) objCampaign.DiscountPercentage = campaignRequest.DiscountPercentage;
                    if (objCampaign.PricePerDay != null) objCampaign.PricePerDay = campaignRequest.PricePerDay;
                    if (objCampaign.Active != null) objCampaign.Active = campaignRequest.Active;

                    _context.Update(objCampaign);
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
        /// Deletes a campaign
        /// </summary>
        /// <param name="campaignId">The unique id of the campaign that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteCampaign(int campaignId)
        {
            try
            {
                bool success = false;
                var objCampaign = _context.Campaigns.FirstOrDefault(a => a.CampaignId == campaignId);
                if (objCampaign != null)
                {
                    _context.Remove(objCampaign);
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
        /// Retrieves a specific campaign 
        /// </summary>
        /// <param name="campaignId">The unique id of the campaign</param>
        /// <returns>The campaign object</returns>
        public Task<CampaignModel> GetCampaignById(int campaignId)
        {
            try
            {
                CampaignModel campaignModel = new CampaignModel();
                var campaign = _context.Campaigns.FirstOrDefault(s => s.CampaignId == campaignId);
                if (campaign != null)
                {
                    campaignModel = _mapper.Map<CampaignModel>(campaign);
                    campaignModel.CampaignTypes = _mapper.Map<List<CampaignTypeModel>>(_context.CampaignTypes.ToList());
                    campaignModel.ReachTypes = _mapper.Map<List<ReachTypeModel>>(_context.ReachTypes.ToList());
                    campaignModel.PeriodTypes = _mapper.Map<List<PeriodTypeModel>>(_context.PeriodTypes.ToList());
                }

                return Task.FromResult(campaignModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

