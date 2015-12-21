using OfficeManagement.Models;
using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class FormController : Controller
    {
        FormService fService = new FormService();
        FormAndFormStatusService ffsService = new FormAndFormStatusService();
        UserService uService = new UserService();
        FormTypeService ftService = new FormTypeService();
        FormAssignedUserService fauService = new FormAssignedUserService();
       

        public ActionResult Index()
        {
            if(Session["UserId"]!=null)
            {
                ViewBag.AllForms = fService.GetAllForms();
                return View();
            }
            else
                return RedirectToAction("", "Login");
            
        }

        public ActionResult Detail(int id)
        {
            if (Session["UserId"] != null)
            {

                int userId = (int)Session["UserId"];
                List<int> assignedUserIdList = fauService.GetAssignedUserId(id);
                ViewBag.UserNameList = uService.GetUserNameList();
                FormAndFormStatusModel formStatus = ffsService.GetFormAndFormStatusData(id, userId);
                formStatus.Form.AssignedUserList= uService.GetAssignedUserNameList(assignedUserIdList);
                return View(formStatus);
            }
            else
                return RedirectToAction("", "Login");
        }

        public ActionResult GetFormByName(string searchString)
        {
            if(Session["UserId"]!=null)
            {
                return PartialView("_partialFormList", fService.GetFormByName(searchString));
            }
            else
                return RedirectToAction("", "Login");
        }

        [HttpPost]
        public bool AddFormStatus(int formId, int isApproved, string statusDescription)
        {
            int userId=(int)Session["UserId"];
            return ffsService.InsertFormStatus(formId, isApproved, statusDescription, userId);
        }

        [HttpPost]
        public bool UpdateForm(FormModel form)
        {
            return fService.FormAndFormAssignedUserUpdate(form);
        }

        public ActionResult Leave()
        {
            if (Session["UserId"] != null)
            {

                ViewBag.ManagerList = uService.GetManagerList();
                List<FormModel> formTypeList = fService.GetFormData(1);
                //List<FormAssignedUserModel> formTypeList = fService.GetFormData(1);
                foreach (var a in formTypeList)
                {
                    ViewBag.FormTypeList = a.FormTypeName;
                }
                return View();
            }

            else

                return RedirectToAction("", "Login");
        }

        [HttpPost]
        public ActionResult Leave(FormModel model )
        {
            if(Session["UserId"]!=null)
            {
               // model.MeetingDate = DateTime.Now;
                //model.IsApproved = false;
                if (ModelState.IsValid)
                {
                    model.CreatedUserId = (int)Session["UserId"];
                    model.FormTypeId = ftService.GetFormType().Where(x => x.Type.ToLower() == "leave").SingleOrDefault().FormTypeId;
                    bool result = fService.FormInsert(model);
                    ViewBag.ManagerList = uService.GetManagerList();
                    return RedirectToAction("", "Home");
                }
                else
                {
                    ViewBag.ManagerList = uService.GetManagerList();
                    return View(model);
                }
            }

            else
                return RedirectToAction("", "Login");
           
          
        }

        public ActionResult Meeting()
        {
            if (Session["UserId"] != null)
            {
                ViewBag.UserNames = uService.GetUserNameList();
                return View();
            }
            else
                return RedirectToAction("", "Login");
        }

        [HttpPost]
        public ActionResult Meeting(FormModel model)
        {
            
            if(Session["UserId"]!=null)
            {
                if (ModelState.IsValid)
                {
                    model.CreatedUserId = (int)Session["UserId"];
                    model.CreatedDate = DateTime.Now;
                    model.FormTypeId = ftService.GetFormType().Where(x => x.Type.ToLower() == "meeting").SingleOrDefault().FormTypeId;
                    bool result = fService.FormInsert(model);
                    if (result == true)
                    {
                        Response.Write("success");
                    }
                    else
                    {
                        Response.Write("fail");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.UserNames = uService.GetUserNameList();
                    return View(model);
                }
           
            }

            else
                return RedirectToAction("", "Login");
        }
    }
}
