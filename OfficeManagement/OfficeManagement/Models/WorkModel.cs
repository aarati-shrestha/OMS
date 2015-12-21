using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class WorkModel
    {
        public int WorkId { get; set; }

        [Required(ErrorMessage="Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        public DateTime? Deadline { get; set; }

        public int CreatedUserId { get; set; }
        public bool isApproved { get; set; } 
        [Required(ErrorMessage = "Priority is required.")]
        public int PriorityId { get; set; }

        public string Priority { get; set; }
        public int? WorkStatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}