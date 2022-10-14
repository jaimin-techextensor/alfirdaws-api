using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class ReachTypeService : IReachTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public ReachTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the reach types within the platform
        /// </summary>
        /// <returns>List of reach types</returns>
        public Task<List<ReachTypeModel>> GetReachTypesOverview()
        {
            try
            {
                var reachTypes = (from reachType in _context.ReachTypes
                                  select new ReachTypeModel
                                  {
                                      Name = reachType.Name,
                                      ReachTypeId = reachType.ReachTypeId
                                  }).ToList();

                return Task.FromResult(reachTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new reach type 
        /// </summary>
        /// <param name="reachTypeRequest">The reach type request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateReachType(ReachTypeCreateRequest reachTypeRequest)
        {
            try
            {
                Response response = new Response();

                var objReachType = new ReachType();
                objReachType.Name = reachTypeRequest.Name;

                var existReachType = _context.ReachTypes.FirstOrDefault(a => a.Name.Equals(reachTypeRequest.Name));
                if (existReachType == null)
                {
                    _context.Add(objReachType);
                    _context.SaveChanges();
                    response.Data = objReachType;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Reach Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a reach type
        /// </summary>
        /// <param name="reachTypeRequest">The reach type update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateReachType(ReachTypeUpdateRequest reachTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existReachType = _context.ReachTypes.FirstOrDefault(a => a.Name.Equals(reachTypeRequest.Name) && a.ReachTypeId != reachTypeRequest.ReachTypeId);
                if (existReachType == null)
                {
                    var objReachType = _context.ReachTypes.FirstOrDefault(a => a.ReachTypeId == reachTypeRequest.ReachTypeId);
                    if (objReachType != null)
                    {
                        if (objReachType.Name != null) objReachType.Name = reachTypeRequest.Name;
                        _context.Update(objReachType);
                        _context.SaveChanges();
                        response.Data = objReachType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Reach Type with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a reach type
        /// </summary>
        /// <param name="reachTypeId">The unique id of the reach type that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteReachType(int reachTypeId)
        {
            try
            {
                bool success = false;
                var objReachType = _context.ReachTypes.FirstOrDefault(a => a.ReachTypeId == reachTypeId);
                if (objReachType != null)
                {
                    _context.Remove(objReachType);
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
        /// Retrieves a specific reach type 
        /// </summary>
        /// <param name="reachTypeId">The unique id of the reach type</param>
        /// <returns>The reach type object</returns>
        public Task<ReachTypeModel> GetReachTypeById(int reachTypeId)
        {
            try
            {
                ReachTypeModel reachTypeModel = new ReachTypeModel();
                var reachType = _context.ReachTypes.FirstOrDefault(s => s.ReachTypeId == reachTypeId);
                if (reachType != null)
                {
                    reachTypeModel = _mapper.Map<ReachTypeModel>(reachType);
                }

                return Task.FromResult(reachTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

