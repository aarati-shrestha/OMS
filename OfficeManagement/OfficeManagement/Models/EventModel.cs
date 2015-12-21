using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int EventToId { get; set; }
        public int[] AssginedUserlist { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}