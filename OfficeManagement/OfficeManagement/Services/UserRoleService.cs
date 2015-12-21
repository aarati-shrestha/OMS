using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace OfficeManagement.Services
{
    public class UserRoleService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public UserRoleModel GetRoleByUserId(int userId)
        {
            var query = from ur in om.UsersRoles
                        join r in om.Roles on ur.RoleId equals r.RoleId
                        where ur.UserId == userId && ur.DeletedDate==null
                        select new UserRoleModel { 
                        UserRoleId=ur.UserRoleId,
                        RoleID=ur.RoleId,
                        role=r.Type
                        };
            return query.SingleOrDefault();
        }
        
    }
}