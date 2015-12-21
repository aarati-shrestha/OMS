using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class EventService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();
        public bool CreateEvent(EventModel model)
        {
            bool status = false;
            try
            {
                Events eventObj = new Events();
                eventObj.Subject = model.Subject;
                eventObj.Body = model.Body;
                eventObj.CreatedDate = DateTime.Now;
                om.Events.Add(eventObj);
                foreach (int e in model.AssginedUserlist)
                {
                    EventTo eventTo = new EventTo();
                    eventTo.EventId = eventObj.EventId;
                    eventTo.UserId = e;
                    om.EventTo.Add(eventTo);
                }
                om.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            
            return status;
        }
    }
}