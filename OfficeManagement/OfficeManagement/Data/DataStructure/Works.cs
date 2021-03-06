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
    
    public partial class Works
    {
        public Works()
        {
            this.WorksUsersStatus = new HashSet<WorksUsersStatus>();
        }
    
        public int WorkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public int CreatedUserId { get; set; }
        public int PriorityId { get; set; }
        public Nullable<int> WorkStatusId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual Priorities Priorities { get; set; }
        public virtual Users Users { get; set; }
        public virtual WorkStatuses WorkStatuses { get; set; }
        public virtual ICollection<WorksUsersStatus> WorksUsersStatus { get; set; }
    }
}
