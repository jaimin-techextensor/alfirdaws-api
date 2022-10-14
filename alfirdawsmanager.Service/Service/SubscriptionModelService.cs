using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace alfirdawsmanager.Service.Service
{
    public class SubscriptionModelService : ISubscriptionModelInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public SubscriptionModelService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the subscription models within the platform
        /// </summary>
        /// <returns>List of subscription models</returns>
        public PagedList<Models.SubscriptionModel> GetSubscriptionModelsOverview(PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(pageParamsRequestModel.SearchText) && pageParamsRequestModel.SearchText != "null")
                {
                    var list = (from subscriptionModel in _context.SubscriptionModels
                                where (subscriptionModel.Name != null && subscriptionModel.Name != null && subscriptionModel.Name.Contains(pageParamsRequestModel.SearchText)
                                                                          || subscriptionModel.SubscriptionType != null && subscriptionModel.SubscriptionType != null && subscriptionModel.SubscriptionType.Contains(pageParamsRequestModel.SearchText)
                                                                          || subscriptionModel.UserType != null && subscriptionModel.UserType != null && subscriptionModel.UserType.Contains(pageParamsRequestModel.SearchText))
                                select new Models.SubscriptionModel
                                {
                                    Visual = subscriptionModel.Visual,
                                    Name = subscriptionModel.Name,
                                    UserType = subscriptionModel.UserType,
                                    SubscriptionType = subscriptionModel.SubscriptionType,
                                    NrOfAds = subscriptionModel.NrOfAds,
                                    NrOfPictures = subscriptionModel.NrOfPictures,
                                    Active = subscriptionModel.Active,
                                    SubscriptionModelId = subscriptionModel.SubscriptionModelId
                                }).ToList();

                    var dataToReturn = PagedList<Models.SubscriptionModel>.ToPagedList(list, pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);

                    return dataToReturn;
                }
                else
                {
                    var list = (from subscriptionModel in _context.SubscriptionModels
                                select new Models.SubscriptionModel
                                {
                                    Visual = subscriptionModel.Visual,
                                    Name = subscriptionModel.Name,
                                    UserType = subscriptionModel.UserType,
                                    SubscriptionType = subscriptionModel.SubscriptionType,
                                    NrOfAds = subscriptionModel.NrOfAds,
                                    NrOfPictures = subscriptionModel.NrOfPictures,
                                    Active = subscriptionModel.Active,
                                    SubscriptionModelId = subscriptionModel.SubscriptionModelId
                                }).ToList();

                    var dataToReturn = PagedList<Models.SubscriptionModel>.ToPagedList(list, pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);

                    return dataToReturn;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new subscription model 
        /// </summary>
        /// <param name="subscriptionModelRequest">The subscription model request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateSubscriptionModel(SubscriptionModelCreateRequest subscriptionModelRequest)
        {
            try
            {
                Response response = new Response();

                var objSubscriptionModel = new Data.Models.SubscriptionModel();
                objSubscriptionModel.SubscriptionType = subscriptionModelRequest.SubscriptionType;
                objSubscriptionModel.UserType = subscriptionModelRequest.UserType;
                objSubscriptionModel.Active = subscriptionModelRequest.Active;
                objSubscriptionModel.IsBasicCampaigns = subscriptionModelRequest.IsBasicCampaigns;
                objSubscriptionModel.IsSocialMedia = subscriptionModelRequest.IsSocialMedia;
                objSubscriptionModel.IsTrends = subscriptionModelRequest.IsTrends;
                objSubscriptionModel.IsOnlineSupport = subscriptionModelRequest.IsOnlineSupport;
                objSubscriptionModel.IsExtendedCampaigns = subscriptionModelRequest.IsExtendedCampaigns;
                objSubscriptionModel.IsPartnership = subscriptionModelRequest.IsPartnership;
                objSubscriptionModel.IsSearchEngine = subscriptionModelRequest.IsSearchEngine;
                objSubscriptionModel.IsStatistics = subscriptionModelRequest.IsStatistics;
                objSubscriptionModel.IsVouchers = subscriptionModelRequest.IsVouchers;
                objSubscriptionModel.Name = subscriptionModelRequest.Name;
                objSubscriptionModel.NrOfAds = subscriptionModelRequest.NrOfAds;
                objSubscriptionModel.NrOfPictures = subscriptionModelRequest.NrOfPictures;
                objSubscriptionModel.UnlimitedPictures = subscriptionModelRequest.UnlimitedPictures;
                objSubscriptionModel.UnlimitedAds = subscriptionModelRequest.UnlimitedAds;
                objSubscriptionModel.Visual = subscriptionModelRequest.Visual;
                objSubscriptionModel.Description = subscriptionModelRequest.Description;

                _context.Add(objSubscriptionModel);
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
        /// Updates a subscription model
        /// </summary>
        /// <param name="campaignRequest">The subscription model update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateSubscriptionModel(SubscriptionModelUpdateRequest subscriptionModelRequest)
        {
            try
            {
                Response response = new Response();
                var objSubscriptionModel = _context.SubscriptionModels.FirstOrDefault(a => a.SubscriptionModelId == subscriptionModelRequest.SubscriptionModelId);
                if (objSubscriptionModel != null)
                {
                    if (objSubscriptionModel.SubscriptionType != null) objSubscriptionModel.SubscriptionType = subscriptionModelRequest.SubscriptionType;
                    if (objSubscriptionModel.UserType != null) objSubscriptionModel.UserType = subscriptionModelRequest.UserType;
                    if (objSubscriptionModel.Active != null) objSubscriptionModel.Active = subscriptionModelRequest.Active;
                    if (objSubscriptionModel.IsBasicCampaigns != null) objSubscriptionModel.IsBasicCampaigns = subscriptionModelRequest.IsBasicCampaigns;
                    if (objSubscriptionModel.IsSocialMedia != null) objSubscriptionModel.IsSocialMedia = subscriptionModelRequest.IsSocialMedia;
                    if (objSubscriptionModel.IsTrends != null) objSubscriptionModel.IsTrends = subscriptionModelRequest.IsTrends;
                    if (objSubscriptionModel.IsOnlineSupport != null) objSubscriptionModel.IsOnlineSupport = subscriptionModelRequest.IsOnlineSupport;
                    if (objSubscriptionModel.IsExtendedCampaigns != null) objSubscriptionModel.IsExtendedCampaigns = subscriptionModelRequest.IsExtendedCampaigns;
                    if (objSubscriptionModel.IsPartnership != null) objSubscriptionModel.IsPartnership = subscriptionModelRequest.IsPartnership;
                    if (objSubscriptionModel.IsSearchEngine != null) objSubscriptionModel.IsSearchEngine = subscriptionModelRequest.IsSearchEngine;
                    if (objSubscriptionModel.IsStatistics != null) objSubscriptionModel.IsStatistics = subscriptionModelRequest.IsStatistics;
                    if (objSubscriptionModel.IsVouchers != null) objSubscriptionModel.IsVouchers = subscriptionModelRequest.IsVouchers;
                    if (objSubscriptionModel.Name != null) objSubscriptionModel.Name = subscriptionModelRequest.Name;
                    if (objSubscriptionModel.NrOfAds != null) objSubscriptionModel.NrOfAds = subscriptionModelRequest.NrOfAds;
                    if (objSubscriptionModel.NrOfPictures != null) objSubscriptionModel.NrOfPictures = subscriptionModelRequest.NrOfPictures;
                    if (objSubscriptionModel.UnlimitedPictures != null) objSubscriptionModel.UnlimitedPictures = subscriptionModelRequest.UnlimitedPictures;
                    if (objSubscriptionModel.UnlimitedAds != null) objSubscriptionModel.UnlimitedAds = subscriptionModelRequest.UnlimitedAds;
                    if (objSubscriptionModel.Description != null) objSubscriptionModel.Description = subscriptionModelRequest.Description;
                    if (objSubscriptionModel.Visual != null) objSubscriptionModel.Visual = subscriptionModelRequest.Visual;

                    _context.Update(objSubscriptionModel);
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
        /// Deletes a subsciption model
        /// </summary>
        /// <param name="subscriptionModelId">The unique id of the subsciption model that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteSubscriptionModel(int subscriptionModelId)
        {
            try
            {
                bool success = false;
                var objSubscriptionModel = _context.SubscriptionModels.FirstOrDefault(a => a.SubscriptionModelId == subscriptionModelId);
                if (objSubscriptionModel != null)
                {
                    _context.Remove(objSubscriptionModel);
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
        /// Retrieves a specific subsciption model 
        /// </summary>
        /// <param name="subscriptionModelId">The unique id of the subsciption model</param>
        /// <returns>The subsciption model object</returns>
        public Task<Models.SubscriptionModel> GetSubscriptionModelById(int subscriptionModelId)
        {
            try
            {
                Models.SubscriptionModel subscriptionModelObj = new Models.SubscriptionModel();
                var subscriptionModel = _context.SubscriptionModels.FirstOrDefault(s => s.SubscriptionModelId == subscriptionModelId);
                if (subscriptionModelObj != null)
                {
                    subscriptionModelObj = _mapper.Map<Models.SubscriptionModel>(subscriptionModel);
                }
                return Task.FromResult(subscriptionModelObj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

