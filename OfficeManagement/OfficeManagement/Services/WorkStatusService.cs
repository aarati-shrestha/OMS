using OfficeManagement.Data.DataStructure;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OfficeManagement.Services
{
    public class WorkStatusService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public List<SelectListItem> GetWorkStatusList()
        {
            var query= from ws in om.WorkStatuses
                       select new SelectListItem
                       {
                           Text=ws.Status,
                           Value=SqlFunctions.StringConvert((double)ws.WorkStatusId).Trim()
                       };
            return query.ToList();
        }

        
    }
}