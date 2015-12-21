using OfficeManagement.Models;
using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class HomeController : Controller
    {

        FormService fService = new FormService();

        public ActionResult Index()
        {
            
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];
                ViewBag.AllForms = fService.GetAllForms();
                return View();
            }
            else
                return RedirectToAction("","Login");
            
            
        }


        public ActionResult GetReceivedFrom(string searchString)
        {
            if(Session["UserId"]!=null)
            {
                return PartialView("_partialReceivedFormList", fService.ReceivedForm((int)Session["UserId"], searchString));
       
            }
            else
                return RedirectToAction("", "Login");

             }

        public ActionResult GetSentForm(string searchString)
        {
            if(Session["UserId"]!=null)
            {
                return PartialView("_partialSentFormList", fService.SentForm((int)Session["UserId"], searchString));
            }
            else
                return RedirectToAction("", "Login");
               }
        [HttpPost]
        public bool DeleteFormByFormId(string[] formId)
        {
            bool result = fService.DeleteFormByFormId(formId);
            return result;
        }
    }
}
