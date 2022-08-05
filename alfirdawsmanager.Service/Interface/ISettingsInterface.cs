using alfirdawsmanager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Interface
{
    public interface ISettingsInterface
    {
        Task<SettingsCounterModel> GetSettingsCounters();
    }
}
