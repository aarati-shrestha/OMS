﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Models
{
    public class FormTypeModel
    {
        public int FormTypeId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}