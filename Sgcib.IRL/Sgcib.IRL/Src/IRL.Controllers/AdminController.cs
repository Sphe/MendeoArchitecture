using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IRL.Services.Contracts;
using IRL.Dtos;
using AutoMapper;
using IRL.Model.AdventureWorks;
using IRL.Model.Security;
using Common.Web.Mvc.JsonNet;
using Common.Web.Mvc.Castle.Contract;

namespace IRL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IWebsitePermissionService websitePermissionService;
        private readonly IWindsorEnumeratorTool windsorEnumeratorTool;

        public AdminController(IRoleService roleService,
                                IUserService userService,
                                IUserRoleService userRoleService,
                                IWebsitePermissionService websitePermissionService,
                                IWindsorEnumeratorTool windsorEnumeratorTool)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.userRoleService = userRoleService;
            this.websitePermissionService = websitePermissionService;
            this.windsorEnumeratorTool = windsorEnumeratorTool;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region User CRUD
        [HttpGet]
        public ActionResult AddUserPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult UpdateUserPartial(Guid id)
        {
            var userToEdit = userService.GetUserById(id);
            var userToEditDto = userToEdit.GenericMapper<User, UserDto>();
            return PartialView(userToEditDto);
        }

        [HttpPost]
        public ActionResult AddUserProcess(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddUserPartial");
            }

            var userEntity = user.GenericMapper<UserDto, User>();
            userService.CreateUser(userEntity);
            
            return Json(new { Response = "success" });
        }

        public ActionResult UpdateUserProcess(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UpdateUserPartial");
            }

            var userEntity = user.GenericMapper<UserDto, User>();
            userService.UpdateUser(userEntity);

            return Json(new { Response = "success" });
        }

        [HttpPost]
        public ActionResult DeleteUserProccess(Guid id)
        {
            userService.DeleteUser(id);
            return Json(new { Response = "success" });
        }

        [HttpGet]
        public ActionResult UserListJson()
        {
            var userListDto = userService.GetAllUsers().GenericMapper<IList<User>, IList<UserDto>>();

            return Json(new JsonEncapsulor<UserDto>() { DataList = userListDto }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Role CRUD
        [HttpGet]
        public ActionResult AddRolePartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult UpdateRolePartial(Guid id)
        {
            var roleToEdit = roleService.GetRoleById(id);
            var roleToEditDto = roleToEdit.GenericMapper<Role, RoleDto>();
            return PartialView(roleToEditDto);
        }

        [HttpPost]
        public ActionResult AddRoleProcess(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddRolePartial");
            }

            var roleEntity = role.GenericMapper<RoleDto, Role>();
            roleService.CreateRole(roleEntity);

            return Json(new { Response = "success" });
        }

        public ActionResult UpdateRoleProcess(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UpdateRolePartial");
            }

            var roleEntity = role.GenericMapper<RoleDto, Role>();
            roleService.UpdateRole(roleEntity);

            return Json(new { Response = "success" });
        }

        [HttpPost]
        public ActionResult DeleteRoleProccess(Guid id)
        {
            roleService.DeleteRole(id);
            return Json(new { Response = "success" });
        }
        
        [HttpGet]
        public ActionResult RoleListJson()
        {
            var roleListDto = roleService.GetAllRoles().GenericMapper<IList<Role>, IList<RoleDto>>();

            return Json(new JsonEncapsulor<RoleDto>() { DataList = roleListDto }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UserInRole CRUD
        [HttpGet]
        public ActionResult AddUserRolePartial()
        {
            var userRoleDto = new UserRoleDto();

            userRoleDto.UserList = userService.GetAllUsers().GenericMapper<IList<User>, IList<UserDto>>();
            userRoleDto.RoleList = roleService.GetAllRoles().GenericMapper<IList<Role>, IList<RoleDto>>();

            return PartialView(userRoleDto);
        }

        [HttpGet]
        public ActionResult UpdateUserRolePartial(int id)
        {
            var userRoleToEdit = userRoleService.GetUserRoleById(id);
            var userRoleToEditDto = userRoleToEdit.GenericMapper<UserRole, UserRoleDto>();

            userRoleToEditDto.UserList = userService.GetAllUsers().GenericMapper<IList<User>, IList<UserDto>>();
            userRoleToEditDto.RoleList = roleService.GetAllRoles().GenericMapper<IList<Role>, IList<RoleDto>>();

            return PartialView(userRoleToEditDto);
        }

        [HttpPost]
        public ActionResult AddUserRoleProcess(UserRoleDto userRole)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddUserRolePartial");
            }

            var userRoleEntity = userRole.GenericMapper<UserRoleDto, UserRole>();
            userRoleService.CreateUserRole(userRoleEntity);

            return Json(new { Response = "success" });
        }

        public ActionResult UpdateUserRoleProcess(UserRoleDto userRole)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UpdateUserRolePartial");
            }

            var userRoleEntity = userRole.GenericMapper<UserRoleDto, UserRole>();
            userRoleService.UpdateUserRole(userRoleEntity);

            return Json(new { Response = "success" });
        }

        [HttpPost]
        public ActionResult DeleteUserRoleProccess(int id)
        {
            userRoleService.DeleteUserRole(id);
            return Json(new { Response = "success" });
        }

        [HttpGet]
        public ActionResult UserRoleListJson()
        {
            var userRoleListDto = userRoleService.GetAllUserRoles().GenericMapper<IList<UserRole>, IList<UserRoleDto>>();

            return Json(new JsonEncapsulor<UserRoleDto>() { DataList = userRoleListDto }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region WebsitePermission CRUD
        [HttpGet]
        public ActionResult AddWebsitePermissionPartial()
        {
            var websitePermissionDto = new WebSiteAccessPermissionDto();

            websitePermissionDto.RoleList = roleService.GetAllRoles().GenericMapper<IList<Role>, IList<RoleDto>>();
            websitePermissionDto.ControllerList = windsorEnumeratorTool.GetAllControllerNames();

            return PartialView(websitePermissionDto);
        }

        [HttpGet]
        public ActionResult UpdateWebsitePermissionPartial(int id)
        {
            var websitePermissionToEdit = websitePermissionService.GetWebsitePermissionById(id);
            var websitePermissionToEditDto = websitePermissionToEdit
                                                .GenericMapper<WebSiteAccessPermission, WebSiteAccessPermissionDto>();

            websitePermissionToEditDto.RoleList = roleService.GetAllRoles().GenericMapper<IList<Role>, IList<RoleDto>>();
            websitePermissionToEditDto.ControllerList = windsorEnumeratorTool.GetAllControllerNames();

            return PartialView(websitePermissionToEditDto);
        }

        [HttpPost]
        public ActionResult AddWebsitePermissionProcess(WebSiteAccessPermissionDto websitePermission)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddWebsitePermissionPartial");
            }

            var websitePermissionEntity = websitePermission.GenericMapper<WebSiteAccessPermissionDto, WebSiteAccessPermission>();
            websitePermissionService.CreateWebsitePermission(websitePermissionEntity);

            return Json(new { Response = "success" });
        }

        public ActionResult UpdateWebsitePermissionProcess(WebSiteAccessPermissionDto websitePermission)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UpdateWebsitePermissionPartial");
            }

            var websitePermissionEntity = websitePermission.GenericMapper<WebSiteAccessPermissionDto, WebSiteAccessPermission>();
            websitePermissionService.UpdateWebsitePermission(websitePermissionEntity);

            return Json(new { Response = "success" });
        }

        [HttpPost]
        public ActionResult DeleteWebsitePermissionProccess(int id)
        {
            websitePermissionService.DeleteWebsitePermission(id);
            return Json(new { Response = "success" });
        }

        [HttpGet]
        public ActionResult WebsitePermissionListJson()
        {
            var websitePermissionListDto = websitePermissionService.GetAllWebsitePermissions()
                                            .GenericMapper<IList<WebSiteAccessPermission>, IList<WebSiteAccessPermissionDto>>();

            return Json(new JsonEncapsulor<WebSiteAccessPermissionDto>() { DataList = websitePermissionListDto }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ActionResultListJson(string ctl)
        {
            var listActionResults = windsorEnumeratorTool.GetAllActionNames(ctl).OrderBy(x => x).ToList();

            listActionResults.Insert(0, string.Empty);

            return Json(new JsonEncapsulor<string>() { DataList = listActionResults }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
