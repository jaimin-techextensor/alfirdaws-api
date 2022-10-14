using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class ModuleService : IModuleInterface
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
        public Task<List<ModuleModel>> GetModulesOverview()
        {
            try
            {
                var list = (from module in _context.Modules
                            select new ModuleModel
                            {
                                Description = module.Description,
                                ModuleId = module.ModuleId,
                                Name = module.Name
                            }).ToList();

                return Task.FromResult(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

