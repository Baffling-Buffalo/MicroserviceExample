using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Filters
{
    /// <summary>
    /// Used to bypass ModelValidationFilter
    /// </summary>
    public class NoReturnOnModelInvalid : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Dont need
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
