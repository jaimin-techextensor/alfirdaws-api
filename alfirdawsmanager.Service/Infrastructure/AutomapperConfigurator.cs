using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using AutoMapper;

namespace alfirdawsmanager.Service.Infrastructure
{
    public class AutomapperConfigurator : Profile
    {
        public AutomapperConfigurator()
        {
            CreateMap<User, UserModel>().ReverseMap(); //reverse so the both direction
            CreateMap<User, UserModelResponse>().ReverseMap(); //reverse so the both direction
            CreateMap<Role, RoleModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Module, ModuleModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Permission, PermissionsModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Category, CategoryModel>().ReverseMap(); //reverse so the both direction
            CreateMap<SubCategory, SubCategoryModel>().ReverseMap(); //reverse so the both direction
            CreateMap<CampaignType, CampaignTypeModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Region, RegionModel>().ReverseMap(); //reverse so the both direction
            CreateMap<ReachType, ReachTypeModel>().ReverseMap(); //reverse so the both direction
        }
    }
}
