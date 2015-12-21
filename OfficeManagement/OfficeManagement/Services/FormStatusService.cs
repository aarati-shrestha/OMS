using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class FormStatusService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public bool FormStatusInsert(FormStatusModel model)
        {
            FormStatuses formStatus = new FormStatuses();
            formStatus.FormId = model.FormId;
            formStatus.UserId = model.UserId;
            formStatus.Description = model.Description;
            formStatus.CreatedDate = model.CreatedDate;
            om.FormStatuses.Add(formStatus);
            om.SaveChanges();
            return true;
        }
    }
}