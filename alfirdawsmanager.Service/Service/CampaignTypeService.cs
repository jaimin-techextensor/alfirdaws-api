using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace alfirdawsmanager.Service.Service
{
    public class CampaignTypeService : ICampaignTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public CampaignTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the campaign types within the platform
        /// </summary>
        /// <returns>List of campaign types</returns>
        public Task<List<CampaignTypeModel>> GetCampaignTypesOverview()
        {
            try
            {
                var campaignTypes = (from campaignType in _context.CampaignTypes
                           select new CampaignTypeModel
                            {
                               Name = campaignType.Name,
                               CampaignTypeId = campaignType.CampaignTypeId
                           }).ToList();
                
                return Task.FromResult(campaignTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new campaign type 
        /// </summary>
        /// <param name="campaignTypeRequest">The campaign type request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateCampaignType(CampaignTypeCreateRequest campaignTypeRequest)
        {
            try
            {
                Response response = new Response();

                var objCampaignType = new CampaignType();
                objCampaignType.Name = campaignTypeRequest.Name;

                var existCampaignType = _context.CampaignTypes.FirstOrDefault(a => a.Name.Equals(campaignTypeRequest.Name));
                if (existCampaignType == null)
                {
                    _context.Add(objCampaignType);
                    _context.SaveChanges();
                    response.Data = objCampaignType;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Campaign Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a campaign type
        /// </summary>
        /// <param name="campaignTypeRequest">The campaign type update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateCampaignType(CampaignTypeUpdateRequest campaignTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existCampaignType = _context.CampaignTypes.FirstOrDefault(a => a.Name.Equals(campaignTypeRequest.Name) && a.CampaignTypeId != campaignTypeRequest.CampaignTypeId);
                if (existCampaignType == null)
                {
                    var objCampaignType = _context.CampaignTypes.FirstOrDefault(a => a.CampaignTypeId == campaignTypeRequest.CampaignTypeId);
                    if (objCampaignType != null)
                    {
                        if (objCampaignType.Name != null) objCampaignType.Name = campaignTypeRequest.Name;
                        _context.Update(objCampaignType);
                        _context.SaveChanges();
                        response.Data = objCampaignType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Campaign Type with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a campaign type
        /// </summary>
        /// <param name="campaignTypeId">The unique id of the campaign type that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteCampaignType(int campaignTypeId)
        {
            try
            {
                bool success = false;
                var objCampaignType = _context.CampaignTypes.FirstOrDefault(a => a.CampaignTypeId == campaignTypeId);
                if (objCampaignType != null)
                {
                    _context.Remove(objCampaignType);
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
        /// Retrieves a specific campaign type 
        /// </summary>
        /// <param name="campaignTypeId">The unique id of the campaign type</param>
        /// <returns>The campaign type object</returns>
        public Task<CampaignTypeModel> GetCampaignTypeById(int campaignTypeId)
        {
            try
            {
                CampaignTypeModel campaignTypeModel = new CampaignTypeModel();
                var campaignType = _context.CampaignTypes.FirstOrDefault(s => s.CampaignTypeId == campaignTypeId);
                if (campaignType != null)
                {
                    campaignTypeModel = _mapper.Map<CampaignTypeModel>(campaignType);
                }

                return Task.FromResult(campaignTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

