//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OfficeManagement.Data.DataStructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkStatuses
    {
        public WorkStatuses()
        {
            this.Works = new HashSet<Works>();
            this.WorksUsersStatus = new HashSet<WorksUsersStatus>();
        }
    
        public int WorkStatusId { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Works> Works { get; set; }
        public virtual ICollection<WorksUsersStatus> WorksUsersStatus { get; set; }
    }
}