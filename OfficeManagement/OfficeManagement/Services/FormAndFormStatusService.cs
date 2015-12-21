using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class FormAndFormStatusService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public FormAndFormStatusModel GetFormAndFormStatusData(int formId, int userId)
        {
            FormAndFormStatusModel form = new FormAndFormStatusModel();
            var query = from f in om.Forms
                        join ft in om.FormTypes on f.FormTypeId equals ft.FormTypeId
                        join cu in om.Users on f.CreatedUserId equals cu.UserId
                        //join au in om.Users on fa.AssignedUserId equals au.UserId
                        where //(fa.AssignedUserId == userId || f.CreatedUserId == userId) &&
                       ( f.FormId == formId && f.DeletedDate==null)
                        select new FormModel
                        {
                            FormId = f.FormId,
                            Name = f.Name,
                            FormTypeName=ft.Type,
                            Description = f.Description,
                            CreatedUserId = f.CreatedUserId,
                            CreatedNameOfUser = cu.FirstName + " " + cu.LastName,
                            FormTypeId=f.FormTypeId,
                            //AssignedUserId = fa.AssignedUserId,
                            //AssignedNameOfUser = au.FirstName + " " + au.LastName,
                            CreatedDate = f.CreatedDate,
                            ModifiedDate=f.ModifiedDate,
                            MeetingDate=f.MeetingDate,
                            //IsApproved = (bool)f.IsApproved

                        };
            form.Form = query.SingleOrDefault();

            var query1 = from fs in om.FormStatuses
                         join u in om.Users on fs.UserId equals u.UserId
                         where (fs.FormId == formId && fs.DeletedDate==null)
                         orderby fs.CreatedDate descending
                         select new FormStatusModel
                         {
                             FormStatusId = fs.FormStatusId,
                             Description = fs.Description,
                             CreatedDate=fs.CreatedDate,
                             userName=u.FirstName+" "+u.LastName,
                             IsApproved=fs.IsApproved
                         };

            form.FormStatus = query1.ToList();
            return form;

        }

        public bool InsertFormStatus(int formId, int isApproved, string statusDescription, int userId)
        {
            bool status = false;
            try
            {

                FormStatuses formStatus = new FormStatuses();
                formStatus.FormId = formId;
                formStatus.UserId = userId;
                formStatus.Description = statusDescription;
                formStatus.CreatedDate = DateTime.Now;
                formStatus.IsApproved = Convert.ToBoolean(isApproved);
                om.FormStatuses.Add(formStatus);
                om.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }


            return status;
        }

        public bool UpdateFormAndFormAssignedUser(FormAndFormStatusModel model)
        {
            bool status = false;
            try
            {
                // Update into forms table
                Forms form = (from f in om.Forms
                              where (f.FormId == model.Form.FormId && f.DeletedDate==null)
                              select f).SingleOrDefault();

                form.Name = model.Form.Name;
                form.Description = model.Form.Description;
               // form.IsApproved = model.Form.IsApproved;
                form.ModifiedDate = DateTime.Now;
                form.MeetingDate = model.Form.MeetingDate;
                om.Forms.Add(form);
                //om.SaveChanges();

                var query = from fau in om.FormsAssignedUsers
                            where fau.FormId == model.Form.FormId 
                            select fau;

                foreach (var d in query)
                {
                    om.FormsAssignedUsers.Remove(d);
                }

                foreach (string e in model.Form.AssignedUserList)
                {
                    FormsAssignedUsers fa = new FormsAssignedUsers();
                    fa.FormId = model.Form.FormId;
                    fa.AssignedUserId = Convert.ToInt32(e);
                    om.FormsAssignedUsers.Add(fa);
                    // om.SaveChanges();

                }

                om.SaveChanges();
                status = true;

            }

            catch (Exception )
            {
                status = false;
            }

            return status;


        }

    }
}