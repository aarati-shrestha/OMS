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
    
    public partial class Menus
    {
        public Menus()
        {
            this.Permissions = new HashSet<Permissions>();
        }
    
        public int MenuId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
