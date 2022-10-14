using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;

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
                settingsCounterModel.usersCount = _context.Users.Select(a => a.UserId).Count();
                settingsCounterModel.rolesCount = _context.Roles.Select(a => a.RoleId).Count();
                settingsCounterModel.modulesCount = _context.Modules.Select(a => a.ModuleId).Count();
                settingsCounterModel.languagesCount = _context.Languages.Select(a => a.LanguageId).Count();
                settingsCounterModel.categoriesCount = _context.Categories.Select(a => a.CategoryId).Count();
                settingsCounterModel.subCategoriesCount = _context.SubCategories.Select(a => a.SubCategoryId).Count();
                settingsCounterModel.countriesCount = _context.Countries.Select(a => a.CountryId).Count();
                settingsCounterModel.regionsCount = _context.Regions.Select(a => a.RegionId).Count();
                settingsCounterModel.campaignsCount = _context.Campaigns.Select(a => a.CampaignId).Count();
                settingsCounterModel.subscriptionModelsCount = _context.SubscriptionModels.Select(a => a.SubscriptionModelId).Count();
                settingsCounterModel.addressTypesCount = _context.AddressTypes.Select(a => a.AddressTypeId).Count();
                settingsCounterModel.paymentTypesCount = _context.PaymentTypes.Select(a => a.PaymentTypeId).Count();
                settingsCounterModel.invoiceTypesCount = _context.InvoiceTypes.Select(a => a.InvoiceTypeId).Count();
                settingsCounterModel.vATTypesCount = _context.Vattypes.Select(a => a.VatTypeId).Count();
                settingsCounterModel.campaignTypesCount = _context.CampaignTypes.Select(a => a.CampaignTypeId).Count();
                settingsCounterModel.reachTypesCount = _context.ReachTypes.Select(a => a.ReachTypeId).Count();
                settingsCounterModel.periodTypesCount = _context.PeriodTypes.Select(a => a.PeriodTypeId).Count();
                settingsCounterModel.pricingModelsCount = _context.PricingModels.Select(a => a.PricingModelId).Count();

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
