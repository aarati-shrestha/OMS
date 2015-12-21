using OfficeManagement.Models;
using OfficeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OfficeManagement
{
    public class Common
    {
        public static List<MenuModel> GetParentMenuList()
        {
            MenuService service = new MenuService();
            List<MenuModel> menuModel = new List<MenuModel>();
            int  roleId = (int)HttpContext.Current.Session["RoleId"];
            menuModel = service.GetMenu(roleId);
            return menuModel;
        }

        public static List<MenuModel> GetChildMenuList( int menuId)
        {
            MenuService service = new MenuService();
            List<MenuModel> menuModel = new List<MenuModel>();
            int roleId = (int)HttpContext.Current.Session["RoleId"];
            menuModel = service.GetChildMenu(menuId,roleId);
            return menuModel;
        }

        public static string ConvertHexStringToBase64(string hexString)
        {
            string source = hexString;
            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            }
            return hash;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}