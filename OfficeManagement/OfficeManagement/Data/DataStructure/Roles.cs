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
    
    public partial class Roles
    {
        public Roles()
        {
            this.Permissions = new HashSet<Permissions>();
            this.UsersRoles = new HashSet<UsersRoles>();
        }
    
        public int RoleId { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual ICollection<Permissions> Permissions { get; set; }
        public virtual Roles Roles1 { get; set; }
        public virtual Roles Roles2 { get; set; }
        public virtual ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
