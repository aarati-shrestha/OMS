using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^(([A-Za-z]+[\s]A-Z''-'\s]*$")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
       // [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

         [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

         [Required]
        public string Gender { get; set; }
         public string Photo { get; set; }
         public HttpPostedFileBase file { get; set; }
        public int RoleId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        
    }
}