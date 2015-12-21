using OfficeManagement.Data.DataStructure;
using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeManagement.Services
{
    public class MenuService
    {
        OfficeManagementSystemEntities om = new OfficeManagementSystemEntities();

        //retrieve all data from user table
        public List<MenuModel> GetMenu(int roleId)
        {


            var query = from m in om.Menus
                        join p in om.Permissions on m.MenuId equals p.MenuId
                        where m.ParentId==null &&
                        p.RoleId==roleId && m.DeletedDate==null && p.DeletedDate==null
                        select new MenuModel
                        {
                           MenuId=m.MenuId,
                           ParentId=m.ParentId,
                           Name=m.Name,
                           Link=m.Link
                          
                        };



            return query.ToList();

        }

        public List<MenuModel> GetChildMenu( int menuId, int roleId)
        {


            var query = from m in om.Menus
                        join p in om.Permissions on m.MenuId equals p.MenuId
                        where m.ParentId == menuId &&
                        p.RoleId == roleId && m.DeletedDate==null && p.DeletedDate==null
                        select new MenuModel
                        {
                            MenuId = m.MenuId,
                            ParentId = m.ParentId,
                            Name = m.Name,
                            Link = m.Link

                        };



            return query.ToList();

        }

       // public List<string> GetMenu
    }
}