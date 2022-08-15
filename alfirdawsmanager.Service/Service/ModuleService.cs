using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class ModuleService: IModuleInterface
    {

        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Constructor of ModuleService
        /// </summary>
        /// <param name="context">The db context parameter</param>
        /// <param name="mapper">The mapper interface</param>
        public ModuleService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrieves the list of all modules
        /// </summary>
        /// <returns>List of module objects</returns>
        public  Task<List<ModuleModel>> GetModulesOverview()
        {
            try
            {
                var dataToReturn = new List<ModuleModel>();
                using (var repo = new RepositoryPattern<Module>())
                {
                    dataToReturn = _mapper.Map<List<ModuleModel>>(repo.SelectAll().OrderBy(a => a.ModuleId).ToList());
                    return Task.FromResult(dataToReturn);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

