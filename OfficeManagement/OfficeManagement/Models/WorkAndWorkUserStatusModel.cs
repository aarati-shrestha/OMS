using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class WorkAndWorkUserStatusModel
    {
        public WorkModel Work { get; set; }
        public WorkUserStatusModel WorkUserStatus { get; set; }
        public List<WorkUserStatusModel> WorkUserStatusList { get; set; }
    }
}