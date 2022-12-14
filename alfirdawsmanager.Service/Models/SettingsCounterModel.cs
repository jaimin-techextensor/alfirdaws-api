using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Models
{
    public class SettingsCounterModel
    {
        public int usersCount { get; set; }
        public int rolesCount { get; set; }
        public int modulesCount { get; set; }
        public int languagesCount { get; set; }
        public int categoriesCount { get; set; }
        public int subCategoriesCount { get; set; }
        public int countriesCount { get; set; }
        public int regionsCount { get; set; }
        public int campaignsCount { get; set; }
        public int subscriptionModelsCount { get; set; }
        public int addressTypesCount { get; set; }
        public int paymentTypesCount { get; set; }
        public int invoiceTypesCount { get; set; }
        public int vATTypesCount { get; set; }
    }
}
