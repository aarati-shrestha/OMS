using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class FormStatusModel
    {
        public int FormStatusId { get; set; }
        public int FormId { get; set; }
        public int UserId { get; set; }
        public string userName { get; set; }
        public bool? IsApproved { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}