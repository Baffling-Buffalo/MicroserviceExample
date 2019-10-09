using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Attributes
{
    public class ViewNameOnModelInvalid : Attribute, IActionFilter
    {
        public string ViewName { get; set; }

        public ViewNameOnModelInvalid(string viewName)
        {
            ViewName = viewName;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Dont need
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
