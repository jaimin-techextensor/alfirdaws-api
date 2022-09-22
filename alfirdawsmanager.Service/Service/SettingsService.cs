using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Service
{
    public class SettingsService: ISettingsInterface
    {
        private readonly AlfirdawsManagerDbContext _context;
        public SettingsService(AlfirdawsManagerDbContext context)
        {
            _context = context;
        }

        #region Methods

        /// <summary>
        /// GetSettingsCounters
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsCounterModel> GetSettingsCounters()
        {
            try
            {
                SettingsCounterModel settingsCounterModel = new SettingsCounterModel();
                settingsCounterModel.usersCount = _context.Users.Count();
                settingsCounterModel.rolesCount = _context.Roles.Count();
                settingsCounterModel.modulesCount = _context.Modules.Count();
                settingsCounterModel.languagesCount = _context.Languages.Count();
                settingsCounterModel.categoriesCount = _context.Categories.Count();
                settingsCounterModel.subCategoriesCount = _context.SubCategories.Count();
                settingsCounterModel.countriesCount = _context.Countries.Count();
                settingsCounterModel.regionsCount = _context.Regions.Count();
                settingsCounterModel.campaignsCount = _context.Campaigns.Count();
                settingsCounterModel.subscriptionModelsCount = _context.SubscriptionModels.Count();
                settingsCounterModel.addressTypesCount = _context.AddressTypes.Count();
                settingsCounterModel.paymentTypesCount = _context.PaymentTypes.Count();
                settingsCounterModel.invoiceTypesCount = _context.InvoiceTypes.Count();
                settingsCounterModel.vATTypesCount = _context.Vattypes.Count();
                settingsCounterModel.campaignTypesCount = _context.CampaignTypes.Count();
                settingsCounterModel.reachTypesCount = _context.ReachTypes.Count();
                settingsCounterModel.periodTypesCount = _context.PeriodTypes.Count();

                return settingsCounterModel;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        #endregion
    }
}
