using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.TagHelpers
{
    [HtmlTargetElement("label",Attributes = "optional")]
    public class OptionalTagHelper : TagHelper
    {
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public OptionalTagHelper(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.sharedLocalizer = sharedLocalizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PostElement.AppendHtml($"<i class='text-muted'>&nbsp({sharedLocalizer["optional"]})</i>");
        }
    }
}
