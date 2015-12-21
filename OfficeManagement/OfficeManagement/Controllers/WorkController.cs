using OfficeManagement.Models;
using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
namespace OfficeManagement.Controllers
{

    public class WorkController : Controller
    {
        UserService uService = new UserService();
        PriorityService pService = new PriorityService();
        WorkService wService = new WorkService();
        WorkStatusService wsService = new WorkStatusService();
        WorkAndWorkUserStatusService wwusService = new WorkAndWorkUserStatusService();
        WorkUserStatusService wusService = new WorkUserStatusService();

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("", "Login");
            }

        }

        public ActionResult GetWorkByTitle(string search, int assignedOrCreated, int currentPage = 1)
        {
            if (Session["UserId"] != null)
            {
                int roleId = (int)Session["RoleId"];
                var query = wService.GetWorkByTitle(search, assignedOrCreated);
                int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                int totalPage = 0;
                int totalRecord = 0;
                totalRecord = query.Count();
                totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
                ViewBag.TotalRecord = totalRecord;
                ViewBag.TotalPage = totalPage;
                ViewBag.CurrentPage = currentPage;
                query = query.OrderBy(s => s.WorkId).Skip(((currentPage - 1) * pageSize)).Take(pageSize);
                return PartialView("_partialWorkList", query.ToList());
            }
            else
            {
                return RedirectToAction("", "Login");
            }
        }
        public ActionResult CreateWork()
        {
            if (Session["UserId"] != null)
            {
                ViewBag.UserList = uService.GetUserNameList();
                ViewBag.PriorityList = pService.GetPriorityList();
                return View();
            }
            else
            {
                return RedirectToAction("", "Login");
            }
        }


        [HttpPost]
        public ActionResult CreateWork(WorkModel model)
        {
            if(Session["UserId"] != null)
            {

                if(ModelState.IsValid)
                {
                    ViewBag.PriorityList = pService.GetPriorityList();
                    int result = wService.InsertOrUpdateWork(model);
                    if (result != 0)
                    {
                        return RedirectToAction("Detail/" + result);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.UserList = uService.GetUserNameList();
                    ViewBag.PriorityList = pService.GetPriorityList();
                    return View(model);
                }
               
            }
            else
            {
                return RedirectToAction("", "Login");
            }
        }

        [HttpPost]
        public int UpdateWork(WorkModel work)
        {
            if (Session["UserId"] != null)
            {
                return wService.InsertOrUpdateWork(work);
            }
             else
            {
                return 0;
            }

        }
        public ActionResult Detail(int id)
        {
            if (Session["UserId"] != null)
            {

                ViewBag.WorkId = id;
                ViewBag.PriorityList = pService.GetPriorityList();
                ViewBag.UserList = uService.GetUserNameList();
                ViewBag.WorkStatusList = wsService.GetWorkStatusList();
                return View(wwusService.GetWorkById(id));
            }
            else
            {
                return RedirectToAction("", "Login");
            }
        }


        public bool InsertWorkUserStatusUpdateWork(WorkUserStatusModel WorkUserStatus)
        {
            bool result = false;
            bool insertResult = wusService.InsertWorkUserStatus(WorkUserStatus);
            int? workId = WorkUserStatus.WorkId;
            int? workStatusId = WorkUserStatus.WorkStatusId;
            bool updateResult = wService.UpdateWorkStatus(workId, workStatusId);
            if (insertResult == true && updateResult == true)
            {
                result = true;
            }
            return result;
        }

        public bool DeleteWorkAndWorkUserStatus(string[] workIds)
        {
            if(workIds==null)
            {
                return false;
            }
            
            
            else
            {
                int[] workId = Array.ConvertAll(workIds, int.Parse);
                return (wwusService.DeleteWorkAndWorkUserStatusById(workId));
            }          
        }
    }
}
