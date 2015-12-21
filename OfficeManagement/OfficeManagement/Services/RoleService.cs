using OfficeManagement.Data.DataStructure;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Services
{
    public class RoleService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public string GetRoleByRoleId(int roleId)
        {
            var query = from r in om.Roles
                        where r.RoleId == roleId && r.DeletedDate==null
                        select r.Type;
            return query.ToString();
        }

        public List<SelectListItem> GetRoleTypeList()
        {
            var query = from r in om.Roles
                        where r.DeletedDate==null
                        select new SelectListItem
                        {
                            Text = r.Type,
                            Value = SqlFunctions.StringConvert((double)r.RoleId).Trim()
                        };
            return query.ToList();
        }
    }
}