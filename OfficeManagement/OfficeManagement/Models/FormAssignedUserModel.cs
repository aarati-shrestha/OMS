using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class FormAssignedUserModel
    {
        public int? FormAssignedUserId { get; set; }
        public int? FormId { get; set; }
        public int? AssignedUserId { get; set; }
    }
}