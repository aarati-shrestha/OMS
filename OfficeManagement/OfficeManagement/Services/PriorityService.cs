using OfficeManagement.Data.DataStructure;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OfficeManagement.Services
{
    public class PriorityService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public List<SelectListItem> GetPriorityList()
        {
            var query = from p in om.Priorities
                        select new SelectListItem
                        {
                            Text = p.Priority,
                            Value = SqlFunctions.StringConvert((double)p.PriorityId).Trim()
                        };
            return query.ToList();
        }

    }
}