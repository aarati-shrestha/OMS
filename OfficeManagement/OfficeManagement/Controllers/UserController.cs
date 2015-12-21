
using OfficeManagement.Models;
using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        UserService uService = new UserService();
        UserRoleService urService = new UserRoleService();
        RoleService rService = new RoleService();
        FormService fService = new FormService();
        ConvertToPdf pdf=new ConvertToPdf();
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {

                return View();
            }
            else
                return RedirectToAction("", "Login");

        }

        public ActionResult GetUserByUsername(string searchString)
        {
            if (Session["UserId"] != null)
            {
                return PartialView("_partialUserList", uService.GetUserByUsername(searchString));
            }
            else
                return RedirectToAction("", "Login");


        }

        public ActionResult Detail(int id)
        {
            if (Session["UserId"] != null)
            {

                UserModel user = uService.GetUserById(id);
                ViewBag.RoleTypeList = rService.GetRoleTypeList();
                return View(user);
            }
            else
                return RedirectToAction("", "Login");

        }

        //public ActionResult Report(int id, DateTime startDate, DateTime endDate)
        //{
        //    return View(fService.GetLeaveByDate(id, startDate, endDate));
        //}
        public ActionResult Report(int id)
        {
            ViewBag.UserId = id;
            return View();
        }
        public ActionResult GetLeaveReport(int userId, string startDate, string endDate)
        {
            return PartialView ("_partialLeaveList", fService.GetLeaveByDate(userId,Convert.ToDateTime(startDate),Convert.ToDateTime(endDate)));
        }

        public void GetPdf(string userId, string startDate, string endDate)
        {
            List<FormModel> listForm = fService.GetLeaveByDate(Convert.ToInt32( userId),Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            pdf.PdfConverter(listForm);
        }

        public bool UpdatePassword(int userId, string password)
        {
            return uService.UpdatePassword(userId, password);
        }

        public bool UpdateUser(UserModel model)
        {
            bool result = uService.UserUpdate(model);
            return result;
        }

        public ActionResult CreateUser()
        {
            if(Session["UserId"]!=null)
            {
                ViewBag.RoleList = rService.GetRoleTypeList();
                return View();
            }
            else
                return RedirectToAction("", "Login");
            
        }
        [HttpPost]
        public ActionResult CreateUser(UserModel user)
        {
            if(Session["UserId"]!=null)
            {
                if (ModelState.IsValid)
                {
                    uService.UserInsert(user);
                    return RedirectToAction("", "User");
                }
                else
                {
                    ViewBag.RoleList = rService.GetRoleTypeList();
                    return View(user);
                }
            }
            else
                return RedirectToAction("", "Login");
           
        }

        [HttpPost]
        public bool DeleteUserById(string[] userId)
        {
            return uService.DeleteUserByUserId(userId);
        }

     
    }
}
