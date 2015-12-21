using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class FormTypeService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public List<FormTypeModel> GetFormType()
        {
            var query = from ft in om.FormTypes
                        where ft.DeletedDate==null
                        select new FormTypeModel
                        {
                            FormTypeId=ft.FormTypeId,
                            Type=ft.Type
                        };
            return query.ToList();
        }
    }
}