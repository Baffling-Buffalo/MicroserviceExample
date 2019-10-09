using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;

namespace WebMVC.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-authorize")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-roles")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-claims")]
    public class AuthorizeTagHelper : TagHelper //, IAuthorizeData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeTagHelper(IHttpContextAccessor httpContextAccessor) // IAuthorizationPolicyProvider policyProvider, IPolicyEvaluator policyEvaluator)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.
        /// </summary>
        [HtmlAttributeName("asp-roles")]
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.
        /// </summary>
        [HtmlAttributeName("asp-claims")]
        public string Claims { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool isAuthorized = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthorized)
            {
                output.SuppressOutput();
            }

            if (Roles != null)
            {
                var requiredRoles = Roles.Split(',');

                foreach (var role in requiredRoles)
                {
                    if(!_httpContextAccessor.HttpContext.User.IsInRole(role)) { 
                        output.SuppressOutput();
                        break;
                    }
                }
            }

            if (Claims != null)
            {
                // Do not check claims if user is admin
                if (_httpContextAccessor.HttpContext.User.IsInRole("admin")) 
                    return;

                var requiredClaims = Claims.Trim().Split(',');

                if(!_httpContextAccessor.HttpContext.User.Claims.Select(cl=>cl.Value).ContainsAllItems(requiredClaims))
                {
                    output.SuppressOutput();
                }
            }

            if (output.Attributes.TryGetAttribute("asp-authorize", out TagHelperAttribute attribute1))
                output.Attributes.Remove(attribute1);
            if (output.Attributes.TryGetAttribute("asp-roles", out TagHelperAttribute attribute2))
                output.Attributes.Remove(attribute2);
            if (output.Attributes.TryGetAttribute("asp-claims", out TagHelperAttribute attribute3))
                output.Attributes.Remove(attribute3);
        }
    }
}
