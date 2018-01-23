using Microsoft.AspNet.Identity;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.IdentityManager.Extension
{
    public static class UserExtensions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
