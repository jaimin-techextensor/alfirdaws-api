using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Infrastructure
{
    public class AutomapperConfigurator : Profile
    {
        public AutomapperConfigurator()
        {
            CreateMap<User, UserModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Role, RoleModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Module, ModuleModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Permission, PermissionsModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Category, CategoryModel>().ReverseMap(); //reverse so the both direction
        }
    }
}
