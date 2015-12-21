using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class LoginController : Controller
    {
        UserService service = new UserService();
        UserRoleService urService = new UserRoleService();

        public ActionResult Index()
        {

            ViewBag.ErrorMessage = "";
            return View();
        }


        [HttpPost]
        public ActionResult Index(string username, string password)
        {
              var data = service.Login(username, password);
                if (data == null)
                {
                    ViewBag.ErrorMessage = "Username/Password is incorrect";
                    return View();
                }
                else
                {
                    Session["UserId"] = data.UserId;
                    Session["Username"] = data.Username;
                    Session["Name"] = data.FirstName + " " + data.LastName;
                    var userRole = urService.GetRoleByUserId(data.UserId);
                    Session["RoleId"] = userRole.RoleID;
                    Session["RoleType"] = userRole.role;
                    return RedirectToAction("Index", "Home");
                }
            


        }

   
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}
