using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class EventToModel
    {
        public int EventToId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}