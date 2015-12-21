using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class UserFormModel
    {
        public UserModel userModel { get; set; }
        public List<FormModel> listForm { get; set; }
    }
}