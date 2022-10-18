using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class CasePriorityService : ICasePriorityInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public CasePriorityService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the case priority within the platform
        /// </summary>
        /// <returns>List of case priority</returns>
        public Task<List<CasePriorityModel>> GetCasePriorityOverview()
        {
            try
            {
                var casePriorities = (from casePriority in _context.CasePriorities
                                      select new CasePriorityModel
                                      {
                                          CasePriorityId = casePriority.CasePriorityId,
                                          Name = casePriority.Name,
                                          Color = casePriority.Color,
                                          Active = casePriority.Active,
                                      }).ToList();

                return Task.FromResult(casePriorities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new case priority 
        /// </summary>
        /// <param name="casePriorityRequest">The case priority request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateCasePriority(CasePriorityCreateRequest casePriorityRequest)
        {
            try
            {
                Response response = new Response();

                var objCasePriority = new CasePriority();
                objCasePriority.Name = casePriorityRequest.Name;
                objCasePriority.Color = casePriorityRequest.Color;
                objCasePriority.Active = casePriorityRequest.Active;

                var existCaseCategory = _context.CaseCategories.FirstOrDefault(a => a.Name.Equals(casePriorityRequest.Name));
                if (existCaseCategory == null)
                {
                    _context.Add(objCasePriority);
                    _context.SaveChanges();
                    response.Data = objCasePriority;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Case priority with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a case priority
        /// </summary>
        /// <param name="casePriorityRequest">The case priority update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateCasePriority(CasePriorityUpdateRequest casePriorityRequest)
        {
            try
            {
                Response response = new Response();
                var existCasePriority = _context.CasePriorities.FirstOrDefault(a => a.Name.Equals(casePriorityRequest.Name) && a.CasePriorityId != casePriorityRequest.CasePriorityId);
                if (existCasePriority == null)
                {
                    var objCasePriority = _context.CasePriorities.FirstOrDefault(a => a.CasePriorityId == casePriorityRequest.CasePriorityId);
                    if (objCasePriority != null)
                    {
                        objCasePriority.Name = casePriorityRequest.Name;
                        objCasePriority.Color = casePriorityRequest.Color;
                        objCasePriority.Active = casePriorityRequest.Active;
                        _context.Update(objCasePriority);
                        _context.SaveChanges();
                        response.Data = objCasePriority;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Case priority with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a case priority
        /// </summary>
        /// <param name="casePriorityId">The unique id of the case priority that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteCasePriority(int casePriorityId)
        {
            try
            {
                bool success = false;
                var objCasePriority = _context.CasePriorities.FirstOrDefault(a => a.CasePriorityId == casePriorityId);
                if (objCasePriority != null)
                {
                    _context.Remove(objCasePriority);
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
        /// Retrieves a specific case priority 
        /// </summary>
        /// <param name="casePriorityId">The unique id of the case priority</param>
        /// <returns>The case category object</returns>
        public Task<CasePriorityModel> GetCasePriorityById(int casePriorityId)
        {
            try
            {
                CasePriorityModel casePriorityModel = new CasePriorityModel();
                var casePriority = _context.CasePriorities.FirstOrDefault(s => s.CasePriorityId == casePriorityId);
                if (casePriority != null)
                {
                    casePriorityModel = _mapper.Map<CasePriorityModel>(casePriority);
                }

                return Task.FromResult(casePriorityModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
