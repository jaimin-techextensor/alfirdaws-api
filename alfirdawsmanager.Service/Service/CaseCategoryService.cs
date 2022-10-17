using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;


namespace alfirdawsmanager.Service.Service
{
    public class CaseCategoryService : ICaseCategoryInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion


        public CaseCategoryService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the case categories within the platform
        /// </summary>
        /// <returns>List of case categories</returns>
        public Task<List<CaseCategoryModel>> GetCaseCategoryOverview()
        {
            try
            {
                var caseCategories = (from caseCategory in _context.CaseCategories
                                 select new CaseCategoryModel
                                 {
                                     CaseCategoryId = caseCategory.CaseCategoryId,
                                     Name = caseCategory.Name,
                                     Active = caseCategory.Active,
                                 }).ToList();

                return Task.FromResult(caseCategories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new case category 
        /// </summary>
        /// <param name="caseCategoryRequest">The case category request</param>
        /// <returns>Response with message and success status</returns>
        public Response CreateCaseCategory(CaseCategoryCreateRequest caseCategoryRequest)
        {
            try
            {
                Response response = new Response();

                var objCaseCategory = new CaseCategory();
                objCaseCategory.Name = caseCategoryRequest.Name;
                objCaseCategory.Active = caseCategoryRequest.Active;

                var existCaseCategory = _context.CaseCategories.FirstOrDefault(a => a.Name.Equals(caseCategoryRequest.Name));
                if (existCaseCategory == null)
                {
                    _context.Add(objCaseCategory);
                    _context.SaveChanges();
                    response.Data = objCaseCategory;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Case Category with same name already exist.";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a case category
        /// </summary>
        /// <param name="caseCategoryRequest">The case category update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public Response UpdateCaseCategory(CaseCategoryUpdateRequest caseCategoryRequest)
        {
            try
            {
                Response response = new Response();
                var existCaseCategory = _context.CaseCategories.FirstOrDefault(a => a.Name.Equals(caseCategoryRequest.Name) && a.CaseCategoryId != caseCategoryRequest.CaseCategoryId);
                if (existCaseCategory == null)
                {
                    var objCaseCategory = _context.CaseCategories.FirstOrDefault(a => a.CaseCategoryId == caseCategoryRequest.CaseCategoryId);
                    if (objCaseCategory != null)
                    {
                        if (objCaseCategory.Name != null) objCaseCategory.Name = caseCategoryRequest.Name;
                        objCaseCategory.Active = caseCategoryRequest.Active;
                        _context.Update(objCaseCategory);
                        _context.SaveChanges();
                        response.Data = objCaseCategory;
                        response.Success = true;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Case Category with same name already exist.";
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a case category
        /// </summary>
        /// <param name="caseCategoryId">The unique id of the case category that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteCaseCategory(int caseCategoryId)
        {
            try
            {
                bool success = false;
                var objCaseCategory = _context.CaseCategories.FirstOrDefault(a => a.CaseCategoryId == caseCategoryId);
                if (objCaseCategory != null)
                {
                    _context.Remove(objCaseCategory);
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
        /// Retrieves a specific case category 
        /// </summary>
        /// <param name="caseCategoryId">The unique id of the case category</param>
        /// <returns>The case category object</returns>
        public Task<CaseCategoryModel> GetCaseCategoryById(int caseCategoryId)
        {
            try
            {
                CaseCategoryModel caseCategoryModel = new CaseCategoryModel();
                var caseCategory = _context.CaseCategories.FirstOrDefault(s => s.CaseCategoryId == caseCategoryId);
                if (caseCategory != null)
                {
                    caseCategoryModel = _mapper.Map<CaseCategoryModel>(caseCategory);
                }

                return Task.FromResult(caseCategoryModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
