using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Pass action names separated by commas. 
        /// Returns true if all passed actions are present in user claims, or if user is in admin role.
        /// </summary>
        /// <param name="user"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool HasActionClaims(this ClaimsPrincipal user, string actionClaims)
        {
            if (user.IsInRole("admin"))
                return true;

            var actionClaimNames = actionClaims.Trim().Split(',').ToList();

            if (user.Claims.Where(c => c.Type == "permission")
                        .Select(c => c.Value)
                        .ToList()
                        .ContainsAllItems(actionClaimNames))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Pass action names separated by commas. 
        /// Returns true if any of the passed actions is present in user claims, or if user is in admin role.
        /// </summary>
        /// <param name="user"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool HasEitherActionClaims(this ClaimsPrincipal user, string actionClaims)
        {
            if (user.IsInRole("admin"))
                return true;

            var actionClaimNames = actionClaims.Trim().Split(',').ToList();

            if (user.Claims.Where(c => c.Type == "permission")
                        .Select(c => c.Value)
                        .Intersect(actionClaimNames)
                        .Any())
                return true;
            else
                return false;
        }
    }
}
