using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class CaseTypeService : ICaseTypeInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public CaseTypeService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the case types within the platform
        /// </summary>
        /// <returns>List of case types</returns>
        public Task<List<CaseTypeModel>> GetCaseTypesOverview()
        {
            try
            {
                var caseTypes = (from caseType in _context.CaseTypes
                           select new CaseTypeModel
                            {
                               CaseTypeId = caseType.CaseTypeId,
                               Name = caseType.Name,
                               Description = caseType.Description,
                               Active = caseType.Active,
                           }).ToList();
                
                return Task.FromResult(caseTypes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new case type 
        /// </summary>
        /// <param name="caseTypeRequest">The case type request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateCaseType(CaseTypeCreateRequest caseTypeRequest)
        {
            try
            {
                Response response = new Response();

                var objCaseType = new CaseType();
                objCaseType.Name = caseTypeRequest.Name;
                objCaseType.Description = caseTypeRequest.Description;
                objCaseType.Active = caseTypeRequest.Active;

                var existCaseType = _context.CaseTypes.FirstOrDefault(a => a.Name.Equals(caseTypeRequest.Name));
                if (existCaseType == null)
                {
                    _context.Add(objCaseType);
                    _context.SaveChanges();
                    response.Data = objCaseType;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Case Type with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a case type
        /// </summary>
        /// <param name="caseTypeRequest">The case type update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateCaseType(CaseTypeUpdateRequest caseTypeRequest)
        {
            try
            {
                Response response = new Response();
                var existCaseType = _context.CaseTypes.FirstOrDefault(a => a.Name.Equals(caseTypeRequest.Name) && a.CaseTypeId != caseTypeRequest.CaseTypeId);
                if (existCaseType == null)
                {
                    var objCaseType = _context.CaseTypes.FirstOrDefault(a => a.CaseTypeId == caseTypeRequest.CaseTypeId);
                    if (objCaseType != null)
                    {
                        if (objCaseType.Name != null) objCaseType.Name = caseTypeRequest.Name;
                        if (objCaseType.Description != null) objCaseType.Description = caseTypeRequest.Description;
                        objCaseType.Active = caseTypeRequest.Active;
                        _context.Update(objCaseType);
                        _context.SaveChanges();
                        response.Data = objCaseType;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Case Type with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a case type
        /// </summary>
        /// <param name="caseTypeId">The unique id of the case type that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteCaseType(int caseTypeId)
        {
            try
            {
                bool success = false;
                var objCaseType = _context.CaseTypes.FirstOrDefault(a => a.CaseTypeId == caseTypeId);
                if (objCaseType != null)
                {
                    _context.Remove(objCaseType);
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
        /// Retrieves a specific case type 
        /// </summary>
        /// <param name="caseTypeId">The unique id of the case type</param>
        /// <returns>The case type object</returns>
        public Task<CaseTypeModel> GetCaseTypeById(int caseTypeId)
        {
            try
            {
                CaseTypeModel caseTypeModel = new CaseTypeModel();
                var caseType = _context.CaseTypes.FirstOrDefault(s => s.CaseTypeId == caseTypeId);
                if (caseType != null)
                {
                    caseTypeModel = _mapper.Map<CaseTypeModel>(caseType);
                }

                return Task.FromResult(caseTypeModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

