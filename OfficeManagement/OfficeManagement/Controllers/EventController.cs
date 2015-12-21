using OfficeManagement.Models;
using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        UserService uService = new UserService();
        EventService eService = new EventService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            ViewBag.EmailList = uService.GetEmailList();
            return View();
        }

        public bool CreateEvent(EventModel Event)
        {
            bool result = eService.CreateEvent(Event);
            return result;
        }

    }
}
