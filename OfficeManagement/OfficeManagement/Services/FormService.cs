using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class FormService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();

        public bool FormInsert(FormModel model)
        {
            bool status = false;
            try
            {
                Forms form = new Forms();
                form.Name = model.Name;
                form.FormTypeId = model.FormTypeId;
                form.Description = model.Description;
                form.CreatedUserId = model.CreatedUserId;
                //form.AssignedUserId = model.AssignedUserId;
                //form.IsApproved = model.IsApproved;
                form.CreatedDate = DateTime.Now;
                if (model.FormTypeId == 2)
                {
                    form.MeetingDate = model.MeetingDate;
                }
                om.Forms.Add(form);
                om.SaveChanges();


                int id = form.FormId;
                foreach (string e in model.AssignedUserList)
                {
                    FormsAssignedUsers fa = new FormsAssignedUsers();
                    fa.FormId = id;
                    fa.AssignedUserId = Convert.ToInt32(e);
                    om.FormsAssignedUsers.Add(fa);

                }
                om.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }


            return status; ;
        }


        public List<FormModel> GetLeaveByDate(int userId, DateTime startingDate, DateTime endingDate)
        {
            var query = from f in om.Forms
                        join u in om.Users on f.CreatedUserId equals u.UserId
                        where f.CreatedUserId == userId && f.FormTypeId == 1 && f.DeletedDate == null
                        && (f.CreatedDate >= startingDate && f.CreatedDate <= endingDate)
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            Description = f.Description,
                            CreatedDate = f.CreatedDate,
                            CreatedNameOfUser = u.FirstName + " " + u.LastName,
                            userImage = u.Photo
                        };
            return query.ToList();
        }

        public List<FormModel> GetAllForms()
        {
            var query = from f in om.Forms
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        where f.DeletedDate == null
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName = ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            CreatedDate = f.CreatedDate,
                            MeetingDate = f.MeetingDate,
                            //IsApproved = (bool)f.IsApproved
                        };
            return query.ToList();
        }

        public List<FormModel> GetFormByName(string searchString)
        {
            var query = from f in om.Forms
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        where (f.DeletedDate == null && f.Name.Contains(searchString))
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName = ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            CreatedDate = f.CreatedDate,
                            MeetingDate = f.MeetingDate,
                            // IsApproved = (bool)f.IsApproved
                        };
            return query.ToList();
        }
        public List<FormModel> GetFormData(int userId)
        {
            var query = from f in om.Forms
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        join fa in om.FormsAssignedUsers on f.FormId equals fa.FormId
                        join au in om.Users on fa.AssignedUserId equals au.UserId//
                        where (fa.AssignedUserId == userId || f.CreatedUserId == userId)
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName = ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            AssignedUserId = fa.AssignedUserId,
                            AssignedNameOfUser = au.FirstName + " " + au.LastName,
                            CreatedDate = f.CreatedDate,
                            MeetingDate = f.MeetingDate,
                            //  IsApproved = (bool)f.IsApproved

                        };

            return query.ToList();

        }

        public List<FormModel> ReceivedForm(int userId, string searchString)
        {
            List<int> receivedFormIdList = (from fau in om.FormsAssignedUsers
                                            where fau.AssignedUserId == userId && fau.DeletedDate == null
                                            select fau.FormId).ToList<int>();
            var assignedUserName = Convert.ToString(HttpContext.Current.Session["Name"]);

            var query = from f in om.Forms
                        from fs in om.FormStatuses.Where(o => f.FormId == o.FormId).OrderByDescending(o => o.CreatedDate)
                                            .Take(1)
                                            .DefaultIfEmpty()
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        where (receivedFormIdList.Contains(f.FormId) &&
                                f.DeletedDate == null && f.Name.Contains(searchString))
                        orderby f.ModifiedDate descending, f.CreatedDate descending
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName = ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            AssignedUserId = userId,
                            AssignedNameOfUser = assignedUserName,
                            CreatedDate = f.CreatedDate,
                            MeetingDate = f.MeetingDate,
                            IsApproved = (fs.IsApproved == null ? false : fs.IsApproved)
                        };
            return query.ToList();

        }

        public List<FormModel> SentForm(int userId, string searchString)
        {
            var query = from f in om.Forms
                        from fs in om.FormStatuses.Where(o => f.FormId == o.FormId).OrderByDescending(o => o.CreatedDate)
                                            .Take(1)
                                            .DefaultIfEmpty()
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        where (f.CreatedUserId == userId &&
                                f.DeletedDate == null && f.Name.Contains(searchString))
                        orderby f.ModifiedDate descending, f.CreatedDate descending
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName = ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            //AssignedUserId = fa.AssignedUserId,
                            //AssignedNameOfUser = au.FirstName + " " + au.LastName,
                            CreatedDate = f.CreatedDate,
                            MeetingDate = f.MeetingDate,
                            IsApproved = (fs.IsApproved == null ? false : fs.IsApproved)

                        };
            return query.ToList();
        }

        public FormModel GetFormById(int formId, int userId)
        {
            var query = from f in om.Forms
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        join fa in om.FormsAssignedUsers on f.FormId equals fa.FormId
                        join au in om.Users on fa.AssignedUserId equals au.UserId
                        where (fa.AssignedUserId == userId || f.CreatedUserId == userId) &&
                        f.FormId == formId
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName = ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            AssignedUserId = fa.AssignedUserId,
                            AssignedNameOfUser = au.FirstName + " " + au.LastName,
                            CreatedDate = f.CreatedDate,
                            MeetingDate = f.MeetingDate

                        };

            return query.SingleOrDefault();

        }

        public bool FormAndFormAssignedUserUpdate(FormModel model)
        {
            bool status = false;
            try
            {
                // Update into forms table
                Forms form = (from f in om.Forms
                              where f.FormId == model.FormId && f.DeletedDate == null
                              select f).SingleOrDefault();

                form.Name = model.Name;
                form.Description = model.Description;
                form.ModifiedDate = DateTime.Now;
                form.MeetingDate = model.MeetingDate;
                //om.Forms.Add(form); 
                //om.SaveChanges();

                var query = from fau in om.FormsAssignedUsers
                            where fau.FormId == model.FormId
                            select fau;

                foreach (var d in query)
                {
                    om.FormsAssignedUsers.Remove(d);
                }

                foreach (string e in model.AssignedUserList)
                {
                    FormsAssignedUsers fa = new FormsAssignedUsers();
                    fa.FormId = model.FormId;
                    fa.AssignedUserId = Convert.ToInt32(e);
                    om.FormsAssignedUsers.Add(fa);
                    // om.SaveChanges();

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

        public bool DeleteFormByFormId(string[] a)
        {
            //string[] b = a;
            int[] formlist = Array.ConvertAll(a, int.Parse);
            bool status = false;
            try
            {
                var query = from f in om.Forms
                            where formlist.Contains(f.FormId)
                            select f;

                foreach (Forms f in query)
                {
                    f.DeletedDate = DateTime.Now;

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