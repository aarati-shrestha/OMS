using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class UserRoleModel
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleID { get; set; }
        public string role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}