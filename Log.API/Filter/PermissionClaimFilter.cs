using Log.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.API.Filter
{
    public class PermissionClaimFilter : Attribute, IActionFilter
    {
        private List<string> Permissions { get; set; }

        /// <summary>
        /// Pass permission claim names
        /// </summary>
        /// <param name="actions"></param>
        public PermissionClaimFilter(string permission1, string permission2 = null, string permission3 = null, string permission4 = null, string permission5 = null) // comma separated action claims
        {
            Permissions = new List<string>();
            Permissions.Add(permission1);
            if (permission2 != null)
                Permissions.Add(permission2);
            if (permission3 != null)
                Permissions.Add(permission3);
            if (permission4 != null)
                Permissions.Add(permission4);
            if (permission5 != null)
                Permissions.Add(permission5);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // if user is admin avoid check
            if (context.HttpContext.User.HasClaim(cl => cl.Type == "role" && cl.Value.ToLower() == "admin"))
                return;

            // check if user has all passed claims
            if (!context.HttpContext.User.Claims.Where(cl => cl.Type == "permission")
                                              .Select(cl => cl.Value)
                                              .ContainsAllItems(Permissions))
                context.Result = new UnauthorizedObjectResult(null);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
