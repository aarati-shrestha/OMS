using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class WorkAndWorkUserStatusService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public int InsertWork(WorkAndWorkUserStatusModel model)
        {
            int workId;
            try
            {
                Works work = new Works();
                work.Title = model.Work.Title;
                work.Description = model.Work.Description;
                work.Deadline = model.Work.Deadline;
                work.CreatedUserId = (int)HttpContext.Current.Session["UserId"];
                work.PriorityId = model.Work.PriorityId;
                work.WorkStatusId = 1;
                work.CreatedDate = DateTime.Now;
                om.Works.Add(work);
                om.SaveChanges();
                workId = work.WorkId;
            }
            catch (Exception)
            {
                workId = 0;
            }

            return workId;
        }

        public WorkAndWorkUserStatusModel GetWorkById(int workId)
        {
            WorkAndWorkUserStatusModel model = new WorkAndWorkUserStatusModel();
            var query = from w in om.Works
                        where w.WorkId == workId
                        select new WorkModel
                        {
                            WorkId = w.WorkId,
                            Title = w.Title,
                            Description = w.Description,
                            Deadline = w.Deadline,
                            PriorityId = w.PriorityId,
                            WorkStatusId = w.WorkStatusId,
                            CreatedDate = w.CreatedDate,
                            CreatedUserId = w.CreatedUserId,
                            ModifiedDate = w.ModifiedDate
                        };
            model.Work = query.SingleOrDefault();
            //model.WorkUserStatusModel
            var queryGetWorkUserStatus = from wus in om.WorksUsersStatus
                                         join ws in om.WorkStatuses on wus.WorkStatusId equals ws.WorkStatusId
                                         join au in om.Users on wus.AssignedUserId equals au.UserId
                                         join u in om.Users on wus.UserId equals u.UserId
                                         where wus.WorkId== workId
                                         orderby ws.WorkStatusId descending
                                         
                                         select new WorkUserStatusModel
                                         {
                                             WorkUserStatusId = wus.WorkUserStatusId,
                                             WorkId = wus.WorkId,
                                             UserId = wus.UserId,
                                             AssignedUserId = wus.AssignedUserId,
                                             assignedUserName = au.FirstName + " " + au.LastName,
                                             userName = u.FirstName + " " + u.LastName,
                                             WorkStatusId = wus.WorkStatusId,
                                             workStatus = ws.Status,
                                             Remark = wus.Remarks
                                         };
            model.WorkUserStatusList = queryGetWorkUserStatus.ToList();
            return model;
        }

        public bool UpdateWorkAndInsertWorkUserStatus(WorkAndWorkUserStatusModel model)
        {
            bool status = false;
            try
            {
                //update work
                Works work = (from w in om.Works
                              where w.WorkId == model.Work.WorkId
                              select w).SingleOrDefault();
                work.Title = model.Work.Title;
                work.Description = model.Work.Description;
                work.Deadline = model.Work.Deadline;
                work.PriorityId = model.Work.PriorityId;
                work.WorkStatusId = model.Work.WorkStatusId;
                work.ModifiedDate = DateTime.Now;

                //insert WorksUsersStatus
                WorksUsersStatus workUserStatus = new WorksUsersStatus();
                workUserStatus.WorkId = model.Work.WorkId;
                workUserStatus.UserId = model.Work.CreatedUserId;
                workUserStatus.WorkStatusId = model.Work.WorkStatusId;
                workUserStatus.AssignedUserId = model.WorkUserStatusList[0].AssignedUserId;
                workUserStatus.Remarks = model.WorkUserStatusList[0].Remark;
                om.WorksUsersStatus.Add(workUserStatus);
                om.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;


        }

        public bool DeleteWorkAndWorkUserStatusById(int[] workId)
        {
            bool status = false;
            try
            {
               List<Works> workList = (from w in om.Works
                              where workId.Contains(w.WorkId)
                              select w).ToList();

               foreach (var work in workList)
               {
                   work.DeletedDate = DateTime.Now;
               }
               
                List<WorksUsersStatus> workUserStatus = (from wus in om.WorksUsersStatus
                                                         where workId.Contains(wus.WorkId)
                                                         select wus).ToList();
                foreach (var w in workUserStatus)
                {
                    w.DeletedDate = DateTime.Now;
                }
                om.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }

            return status;
        }
    }
}