using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class PermissionModel
    {
        public int PermissionId { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
    }
}