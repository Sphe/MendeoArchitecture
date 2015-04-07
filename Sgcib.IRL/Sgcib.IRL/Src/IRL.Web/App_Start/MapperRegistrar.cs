using AutoMapper;
using IRL.Model.AdventureWorks;
using IRL.Dtos;
using IRL.Model.Security;

namespace IRL.Web.App_Start
{
    public class MapperRegistrar
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<FilterArgumentDto, FilterArgument>();

            Mapper.CreateMap<vEmployee, EmployeeDto>();

            Mapper.CreateMap<UserDto, User>();

            Mapper.CreateMap<User, UserDto>();

            Mapper.CreateMap<Role, RoleDto>();

            Mapper.CreateMap<RoleDto, Role>();

            Mapper.CreateMap<UserRole, UserRoleDto>();

            Mapper.CreateMap<UserRoleDto, UserRole>();

            Mapper.CreateMap<WebSiteAccessPermission, WebSiteAccessPermissionDto>();

            Mapper.CreateMap<WebSiteAccessPermissionDto, WebSiteAccessPermission>();
        }
    }
}