using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Services
{
    public class UserService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();

        //retrieve all data from user table
        public List<UserModel> GetUser()
        {


            var query = from u in om.Users
                        join r in om.UsersRoles on u.UserId equals r.UserId
                        where u.DeletedDate==null
                        select new UserModel
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName=u.LastName,
                            Username=u.Username,
                            Password=u.Password,
                            Gender = u.Gender,
                            Photo=u.Photo,
                            DOB = u.DOB,
                            RoleId=r.RoleId,
                            Email=u.Email,
                            CreatedDate =u.CreatedDate,
                            ModifiedDate = (DateTime)u.ModifiedDate,
                            DeletedDate = (DateTime)u.DeletedDate
                            
                        };
            


            return query.ToList();

        }
        //insert user data to database
        public bool UserInsert(UserModel model)
        {
            bool status = false;
            try
            {
                Users user = new Users();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Username = model.Username;
                user.Photo = FileUpload(model.file);
                user.Password =Common.ConvertHexStringToBase64( model.Password);
                user.Gender = model.Gender;
                user.DOB = model.DOB;
                user.CreatedDate = DateTime.Now;
                om.Users.Add(user);

                UsersRoles userRole = new UsersRoles();
                userRole.RoleId = model.RoleId;
                userRole.UserId = model.UserId;
                om.UsersRoles.Add(userRole);
                om.SaveChanges();
                status = true;
            }

            catch(Exception)
            {
                status = false;
            }
            return status;
        }

        public bool UserUpdate(UserModel model)
        {
            bool status = false;
            try
            {
                Users user = (from u in om.Users
                              where u.UserId == model.UserId
                              select u).SingleOrDefault();
                user.Username = model.Username;
                user.Password = Common.ConvertHexStringToBase64( model.Password);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Gender = model.Gender;
                user.DOB = model.DOB;
                user.ModifiedDate = DateTime.Now;
                user.DeletedDate = model.DeletedDate;
                //int a = model.RoleId;

                UsersRoles userRole = (from ur in om.UsersRoles
                                       where ur.UserId == model.UserId
                                       select ur).SingleOrDefault();
                userRole.UserId = model.UserId;
                userRole.RoleId = model.RoleId;
                userRole.ModifiedDate = model.ModifiedDate;
                userRole.DeletedDate = model.DeletedDate;
                om.SaveChanges();
                status = true;
            }

            catch(Exception)
            {
                status = false;
            }
            return status;
        }

       

        public List<UserModel> GetUserByUsername(string searchString)
        {
            var query = from u in om.Users
                        join ur in om.UsersRoles on u.UserId equals ur.UserId
                        where u.Username.Contains(searchString) && u.DeletedDate == null && ur.DeletedDate==null
                        select new UserModel
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.Username,
                            Password = u.Password,
                            Gender = u.Gender,
                            Photo = u.Photo,
                            DOB = u.DOB,
                            RoleId = ur.RoleId,
                            CreatedDate = u.CreatedDate,
                            ModifiedDate = (DateTime)u.ModifiedDate,
                            DeletedDate = (DateTime)u.DeletedDate
                        };
            return query.ToList();
        }


        public bool UpdatePassword( int userId ,string password)
        {
            bool status = false;
            try
            {
                Users user = (from u in om.Users
                              where u.UserId == userId
                              select u).SingleOrDefault();
                user.Password = Common.ConvertHexStringToBase64(password);
                om.SaveChanges();
                status= true;
            }
            catch(Exception)
            {
                status = false;
            }
            return status;
        }

        //User Login
        public UserModel Login(string username, string password)
        {
            password = Common.ConvertHexStringToBase64(password);
            var query = from u in om.Users
                        where u.Username.Equals(username)
                                && u.Password.Equals(password)
                                && u.DeletedDate==null

                        select new UserModel
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.Username,
                            Password = u.Password,
                            Gender = u.Gender,
                            Photo = u.Photo,
                            DOB = (DateTime)u.DOB
                            //CreatedDate = (DateTime)u.CreatedDate,
                            //ModifiedDate = (DateTime)u.ModifiedDate,
                            //DeletedDate = (DateTime)u.DeletedDate

                        };
            return query.SingleOrDefault();
        }

        public List<SelectListItem> GetManagerList()
        {


            var query = from u in om.Users
                        join ur in om.UsersRoles on u.UserId equals ur.UserId
                        join r in om.Roles on ur.RoleId equals r.RoleId
                        where r.Type.ToLower() == "manager" && ur.DeletedDate==null
                        && u.DeletedDate==null && r.DeletedDate==null
                        select new SelectListItem
                        {
                            Text = u.FirstName + " " + u.LastName,
                            Value = SqlFunctions.StringConvert((double)u.UserId).Trim()

                        };

            return query.ToList();

        }

        public List<SelectListItem> GetUserNameList()
        {
            var query = from u in om.Users
                        where u.DeletedDate==null
                        select new SelectListItem
                        {
                           Text=u.FirstName+" "+u.LastName,
                           Value = SqlFunctions.StringConvert((double)u.UserId).Trim()

                        };
            return query.ToList();
           
        }

        public List<SelectListItem> GetEmailList()
        {
            var query = from u in om.Users
                        where u.DeletedDate == null
                        select new SelectListItem
                        {
                            Text = u.Email,
                            Value = SqlFunctions.StringConvert((double)u.UserId).Trim()

                        };
            return query.ToList();

        }

        public UserModel GetUserById(int userId)
        {
            var query = from u in om.Users
                        join ur in om.UsersRoles on u.UserId equals ur.UserId
                        where u.UserId == userId && u.DeletedDate==null && ur.DeletedDate==null
                        select new UserModel
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.Username,
                            Password = u.Password,
                            Gender = u.Gender,
                            //Photo = u.Photo,
                            DOB = u.DOB,
                            RoleId=ur.RoleId,
                            CreatedDate = u.CreatedDate,
                            ModifiedDate = u.ModifiedDate,
                            DeletedDate =u.DeletedDate

                        };
            return query.SingleOrDefault();
        }

        public string[] GetAssignedUserNameList(List<int> assignedUserIdList)
        {
            var query = from u in om.Users
                        where (assignedUserIdList.Contains(u.UserId)&& u.DeletedDate==null)
                        select SqlFunctions.StringConvert((double)u.UserId).Trim();
            return query.ToArray();
        }
        public List<int> GetUserList()
        {
            var query = (from fau in om.FormsAssignedUsers
                         where fau.DeletedDate==null
                         select fau.AssignedUserId).Distinct().ToList<int>();
            return query;

        }

        public bool DeleteUserByUserId(string[] userId)
        {
            //string[] b = a;
            int[] userIdList = Array.ConvertAll(userId, int.Parse);
            bool status = false;
            try
            {
                var query = from u in om.Users
                            where userIdList.Contains(u.UserId) && u.DeletedDate==null
                            select u;
  

                foreach (Users u in query)
                {
                    u.DeletedDate = DateTime.Now;

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

        private string FileUpload(HttpPostedFileBase file)
        {
            string pic = "";
           
            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/images/UserImage"), pic);
                // file is uploaded
                file.SaveAs(path);
                
                //string pic = System.IO.Path.GetFileName(photo.FileName);
                //string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/images/UserImage"), pic);
                // file is uploaded
                //photo.SaveAs(path);
                

            }
            string filePath = "~/images/UserImage/"+pic;
            return filePath;
        }
    }
}