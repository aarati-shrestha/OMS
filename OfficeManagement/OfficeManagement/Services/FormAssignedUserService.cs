using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class FormAssignedUserService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public List<string> GetUserName()
        {
            List<string> fullName = new List<string>();
            var query = from u in om.Users
                        where u.DeletedDate==null
                        select new UserModel
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName

                        };
            query.ToList();
            foreach (UserModel u in query)
            {
                fullName.Add(u.FirstName + " " + u.LastName);
            }
            return fullName;
        }

        public List<int> GetAssignedUserId(int formId)
        {
            var query = (from fau in om.FormsAssignedUsers
                         where fau.FormId == formId && fau.AssignedUserId != null && fau.DeletedDate==null
                         select fau.AssignedUserId).ToList<int>();
            return query;
        }

        public List<int> GetDistinctAssignedUserId()
        {
            var query = (from fau in om.FormsAssignedUsers
                         where fau.DeletedDate==null
                         select fau.AssignedUserId).Distinct().ToList<int>();
            return query;

        }

    }
}