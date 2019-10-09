using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Attributes
{
    public class ModelValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Dont need
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                if (context.ActionDescriptor.FilterDescriptors.Any(fd => fd.Filter.GetType() == typeof(NoReturnOnModelInvalid)))
                    return;

                var viewFilter = (ViewNameOnModelInvalid)context.ActionDescriptor.FilterDescriptors.FirstOrDefault(fd => fd.Filter.GetType() == typeof(ViewNameOnModelInvalid))?.Filter;

                var newResult = new ViewResult()
                {
                    ViewData = ((Controller)context.Controller).ViewData,
                    TempData = ((Controller)context.Controller).TempData,
                    StatusCode = 400
                };
                if (!string.IsNullOrWhiteSpace(viewFilter?.ViewName))
                    newResult.ViewName = viewFilter.ViewName;

                context.Result = newResult;
            }
        }
    }
}
