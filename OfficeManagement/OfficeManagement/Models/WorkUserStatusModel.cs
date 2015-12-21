using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class WorkUserStatusModel
    {
        public int WorkUserStatusId { get; set; }
        public int WorkId { get; set; }
        public int UserId { get; set; }
        public string  userName { get; set; }
        public int? WorkStatusId { get; set; }
        public string workStatus { get; set; }
        public int? AssignedUserId { get; set; }
        public string assignedUserName { get; set; }
        public string Remark { get; set; }
    }
}