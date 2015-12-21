using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class FormModel
    {
        public int FormId { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public int FormTypeId { get; set; }
        public string FormTypeName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedNameOfUser { get; set; }
        public int? AssignedUserId { get; set; }
        [Required(ErrorMessage = "Assigned user/s  are required")]
        public string[] AssignedUserList { get; set; }
        public string AssignedNameOfUser { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? MeetingDate { get; set; }
        public string userImage { get; set; }
    }
}