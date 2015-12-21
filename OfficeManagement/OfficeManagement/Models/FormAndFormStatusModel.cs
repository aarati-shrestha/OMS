using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class FormAndFormStatusModel
    {
        public FormModel Form { get; set; }
        public List<FormStatusModel> FormStatus { get; set; }
    }
}