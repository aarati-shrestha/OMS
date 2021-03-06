﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class Events
{
    public Events()
    {
        this.EventTo = new HashSet<EventTo>();
    }

    public int EventId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Nullable<int> EventToId { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }

    public virtual ICollection<EventTo> EventTo { get; set; }
}

public partial class EventTo
{
    public int EventToId { get; set; }
    public Nullable<int> EventId { get; set; }
    public Nullable<int> UserId { get; set; }

    public virtual Events Events { get; set; }
    public virtual Users Users { get; set; }
}

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

public partial class FormsAssignedUsers
{
    public int FormsAssignedUsersId { get; set; }
    public int FormId { get; set; }
    public int AssignedUserId { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }

    public virtual Forms Forms { get; set; }
}

public partial class FormStatuses
{
    public int FormStatusId { get; set; }
    public int FormId { get; set; }
    public string Description { get; set; }
    public System.DateTime CreatedDate { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }
    public Nullable<int> UserId { get; set; }
    public bool IsApproved { get; set; }

    public virtual Users Users { get; set; }
    public virtual Forms Forms { get; set; }
}

public partial class FormTypes
{
    public FormTypes()
    {
        this.Forms = new HashSet<Forms>();
    }

    public int FormTypeId { get; set; }
    public string Type { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }

    public virtual FormTypes FormTypes1 { get; set; }
    public virtual FormTypes FormTypes2 { get; set; }
    public virtual ICollection<Forms> Forms { get; set; }
}

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

public partial class Permissions
{
    public int PermissionId { get; set; }
    public int RoleId { get; set; }
    public Nullable<int> UserId { get; set; }
    public int MenuId { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }

    public virtual Menus Menus { get; set; }
    public virtual Roles Roles { get; set; }
    public virtual Users Users { get; set; }
}

public partial class Priorities
{
    public Priorities()
    {
        this.Works = new HashSet<Works>();
    }

    public int PriorityId { get; set; }
    public string Priority { get; set; }

    public virtual ICollection<Works> Works { get; set; }
}

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

public partial class sysdiagrams
{
    public string name { get; set; }
    public int principal_id { get; set; }
    public int diagram_id { get; set; }
    public Nullable<int> version { get; set; }
    public byte[] definition { get; set; }
}

public partial class Users
{
    public Users()
    {
        this.UsersRoles = new HashSet<UsersRoles>();
        this.Works = new HashSet<Works>();
        this.WorksUsersStatus = new HashSet<WorksUsersStatus>();
        this.Permissions = new HashSet<Permissions>();
        this.FormStatuses = new HashSet<FormStatuses>();
        this.EventTo = new HashSet<EventTo>();
    }

    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public string Photo { get; set; }
    public Nullable<System.DateTime> DOB { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }
    public string Email { get; set; }

    public virtual ICollection<UsersRoles> UsersRoles { get; set; }
    public virtual ICollection<Works> Works { get; set; }
    public virtual ICollection<WorksUsersStatus> WorksUsersStatus { get; set; }
    public virtual ICollection<Permissions> Permissions { get; set; }
    public virtual ICollection<FormStatuses> FormStatuses { get; set; }
    public virtual ICollection<EventTo> EventTo { get; set; }
}

public partial class UsersRoles
{
    public int UserRoleId { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }

    public virtual Users Users { get; set; }
    public virtual Roles Roles { get; set; }
}

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

public partial class WorksUsersStatus
{
    public int WorkUserStatusId { get; set; }
    public int WorkId { get; set; }
    public int UserId { get; set; }
    public Nullable<int> WorkStatusId { get; set; }
    public Nullable<int> AssignedUserId { get; set; }
    public string Remarks { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<System.DateTime> DeletedDate { get; set; }

    public virtual Users Users { get; set; }
    public virtual Works Works { get; set; }
    public virtual WorkStatuses WorkStatuses { get; set; }
}
