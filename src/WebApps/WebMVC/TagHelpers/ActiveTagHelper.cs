using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebMVC.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "active-url")]
    [HtmlTargetElement("li", Attributes = "active-url")]
    public class ActiveTagHelper : TagHelper
    {
        public IHttpContextAccessor ContextAccessor { get; }

        public string ActiveUrl { get; set; }

        public bool IndexPage { get; set; }

        public ActiveTagHelper(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var path = ContextAccessor.HttpContext.Request.Path.ToString();
            if ((ActiveUrl == "HomePage" && path == "/") || Regex.Match(path, @"\b" + ActiveUrl + @"\b").Success)
            {
                if (IndexPage && path.Split(ActiveUrl)[1].Count() > 1)
                    return;

                var existingAttrs = output.Attributes["class"]?.Value;
                output.Attributes.SetAttribute("class",
                    "active " + existingAttrs.ToString());
            }
        }

    }
}
