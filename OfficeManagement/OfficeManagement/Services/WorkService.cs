using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class WorkService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public int InsertOrUpdateWork(WorkModel model)
        {
            if (model.WorkId == 0)
            {
                int workId;
                try
                {
                    Works work = new Works();
                    work.Title = model.Title;
                    work.Description = model.Description;
                    work.Deadline = model.Deadline;
                    work.CreatedUserId = (int)HttpContext.Current.Session["UserId"];
                    work.PriorityId = model.PriorityId;
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

            else
            {
                Works work = (from f in om.Works
                              where f.WorkId == model.WorkId
                              select f).SingleOrDefault();
                work.Title = model.Title;
                work.Description = model.Description;
                work.Deadline = model.Deadline;
                work.PriorityId = model.PriorityId;
                work.WorkStatusId = model.WorkStatusId;
                work.ModifiedDate = DateTime.Now;
                work.DeletedDate = model.DeletedDate;
                om.SaveChanges();
                return work.WorkId;
            }

        }

        public bool UpdateWorkStatus(int? workId, int? workStatusId)
        {
            bool status = false;
            try
            {
                Works work = (from w in om.Works
                              where w.WorkId == workId
                              select w).SingleOrDefault();
                work.WorkStatusId = workStatusId;
                om.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public WorkModel GetWorkById(int workId)
        {
            var query = from w in om.Works
                        where w.WorkId == workId
                        select new WorkModel
                        {
                            WorkId = w.WorkId,
                            Title = w.Title,
                            Description = w.Title,
                            Deadline = w.Deadline,
                            PriorityId = w.PriorityId,
                            WorkStatusId = w.WorkStatusId,
                            CreatedDate = w.CreatedDate,
                            ModifiedDate = w.ModifiedDate
                        };
            return query.SingleOrDefault();
        }

        //public List<WorkModel> GetAllWork()
        //{
        //    var queryWork = from w in om.Works
        //                    join p in om.Priorities on w.PriorityId equals p.PriorityId
        //                    where w.DeletedDate == null
        //                    select new WorkModel
        //                    {
        //                        WorkId = w.WorkId,
        //                        Title = w.Title,
        //                        Description = w.Description,
        //                        Deadline = w.Deadline,
        //                        PriorityId = w.PriorityId,
        //                        Priority = p.Priority,
        //                        WorkStatusId = w.WorkStatusId,
        //                        CreatedUserId = w.CreatedUserId,
        //                        CreatedDate = w.CreatedDate,
        //                        ModifiedDate = w.ModifiedDate
        //                    };
        //    return queryWork.ToList();
        //}

        public IQueryable<WorkModel> GetWorkByTitle(string title, int assignedOrCreated)
        {
            string role = HttpContext.Current.Session["RoleType"].ToString();
            int userId = (int)HttpContext.Current.Session["UserId"];
            var requiredWorkIds = new List<int>();

            var query = from w in om.Works
                        join p in om.Priorities on w.PriorityId equals p.PriorityId
                        where (w.Title.Contains(title) && w.DeletedDate==null && w.Deadline> DateTime.Now)
                        select new WorkModel
                        {
                            WorkId = w.WorkId,
                            Title = w.Title,
                            Description = w.Description,
                            Deadline = w.Deadline,
                            PriorityId = w.PriorityId,
                            Priority = p.Priority,
                            WorkStatusId = w.WorkStatusId,
                            CreatedUserId = w.CreatedUserId,
                            CreatedDate = w.CreatedDate,
                            ModifiedDate = w.ModifiedDate
                        };

            if (role.ToLower() != "admin" || assignedOrCreated > 0)
            {
                requiredWorkIds = (from wus in om.WorksUsersStatus
                                   where wus.AssignedUserId == userId
                                   select wus.WorkId).ToList();
            }


            if (role.ToLower() == "manager")
            {
                query = query.Where(u => u.CreatedUserId == userId || requiredWorkIds.Contains(u.WorkId));
            }
            else if (role.ToLower() == "employee")
            {
                query = query.Where(u => requiredWorkIds.Contains(u.WorkId));
            }

            if (assignedOrCreated == 1)
            {
                query = query.Where(u => requiredWorkIds.Contains(u.WorkId));
            }
            else if (assignedOrCreated == 2)
            {
                query = query.Where(u => u.CreatedUserId == userId);
            }


            return query;
        }
    }
}