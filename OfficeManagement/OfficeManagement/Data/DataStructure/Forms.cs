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
    
    public partial class Forms
    {
        public Forms()
        {
            this.FormsAssignedUsers = new HashSet<FormsAssignedUsers>();
            this.FormStatuses = new HashSet<FormStatuses>();
        }
    
        public int FormId { get; set; }
        public string Name { get; set; }
        public int FormTypeId { get; set; }
        public string Description { get; set; }
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> MeetingDate { get; set; }
    
        public virtual FormTypes FormTypes { get; set; }
        public virtual ICollection<FormsAssignedUsers> FormsAssignedUsers { get; set; }
        public virtual ICollection<FormStatuses> FormStatuses { get; set; }
    }
}