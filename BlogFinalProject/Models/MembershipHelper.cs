using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFinalProject.Models
{
    public class MembershipHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>
            (new UserStore<ApplicationUser>(db));

        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(db));

        public static List<string> GetAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToList();
        }
        public static List<string> GetAllUsersNames()
        {
            return db.Users.Select(u => u.UserName).ToList();
        }
        public static bool AddNewRole(string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole { Name = roleName });
                return true;
            }
            else
            {
                return false; // false means the operation failed 
            }
        }
        public static IdentityResult RemoveRole(string roleName)
        {
            return roleManager.Delete(new IdentityRole { Name = roleName });
        }
        public static bool CheckUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }
        public static IdentityResult AddUserToRole(string userId, string roleName)
        {
            return userManager.AddToRole(userId, roleName);
        }
        public static List<string> GetAllRolesForUser(string userId)
        {
            return userManager.GetRoles(userId).ToList();
        }
    }
}