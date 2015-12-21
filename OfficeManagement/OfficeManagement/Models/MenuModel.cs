using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}