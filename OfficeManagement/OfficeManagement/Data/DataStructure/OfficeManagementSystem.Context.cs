﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class OfficeManagementSystemEntities : DbContext
{
    public OfficeManagementSystemEntities()
        : base("name=OfficeManagementSystemEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<UsersRoles> UsersRoles { get; set; }
    public DbSet<sysdiagrams> sysdiagrams { get; set; }
    public DbSet<FormTypes> FormTypes { get; set; }
    public DbSet<FormStatuses> FormStatuses { get; set; }
    public DbSet<FormsAssignedUsers> FormsAssignedUsers { get; set; }
    public DbSet<Priorities> Priorities { get; set; }
    public DbSet<WorkStatuses> WorkStatuses { get; set; }
    public DbSet<Menus> Menus { get; set; }
    public DbSet<Works> Works { get; set; }
    public DbSet<WorksUsersStatus> WorksUsersStatus { get; set; }
    public DbSet<Permissions> Permissions { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Forms> Forms { get; set; }
    public DbSet<Events> Events { get; set; }
    public DbSet<EventTo> EventTo { get; set; }
}