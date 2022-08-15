using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;

namespace alfirdawsmanager.Service.Interface
{
    public interface IModuleInterface
    {
        Task<List<ModuleModel>> GetModulesOverview();

    }
}

