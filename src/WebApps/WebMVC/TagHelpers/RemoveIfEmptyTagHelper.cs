using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.TagHelpers
{
    [HtmlTargetElement(Attributes = "remove-if-empty")]
    public class RemoveIfEmptyTagHelper : TagHelper
    {
        public override int Order => 999;

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if ((await output.GetChildContentAsync()).IsEmptyOrWhiteSpace)
                output.SuppressOutput();
        }
    }
}
