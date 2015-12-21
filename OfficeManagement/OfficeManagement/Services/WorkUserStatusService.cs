using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class WorkUserStatusService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public bool InsertWorkUserStatus(WorkUserStatusModel model)
        {
            bool status = false;
            try
            {
                WorksUsersStatus workUserStatus = new WorksUsersStatus();
                workUserStatus.WorkId = model.WorkId;
                workUserStatus.UserId = model.UserId;
                workUserStatus.WorkStatusId = model.WorkStatusId;
                workUserStatus.AssignedUserId = model.AssignedUserId;
                workUserStatus.Remarks = model.Remark;
                om.WorksUsersStatus.Add(workUserStatus);
                om.SaveChanges();
                status = true;
            }
            catch(Exception )
            {
                status = false;
            }
            
            return status;
        }

        public List<WorkUserStatusModel> GetAllWorkUserStatus()
        {
            var query = from f in om.WorksUsersStatus
                        select new WorkUserStatusModel
                        {
                            WorkUserStatusId = f.WorkUserStatusId,
                            WorkId = f.WorkId,
                            UserId = f.UserId,
                            AssignedUserId = f.AssignedUserId,
                            WorkStatusId=f.WorkStatusId,
                            Remark = f.Remarks
                        };
            return query.ToList();
            
        }
    }
}