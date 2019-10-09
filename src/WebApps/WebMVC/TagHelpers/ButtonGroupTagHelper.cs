using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.TagHelpers
{
    //[RestrictChildren("btn-group-link", "divider")]
    //[HtmlTargetElement("btn-group")]
    //public class ButtonGroupTagHelper
    //{
    //    public string Title { get; set; }

    //    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        var btnGrpContext = new ButtonGroupContext();
    //        context.Items.Add(typeof(ButtonGroupTagHelper), btnGrpContext);

    //        await output.GetChildContentAsync();

    //        var template =
    //        $@"<div class='btn-group'>
    //              <button class='btn btn-primary dropdown-toggle' data-toggle='dropdown'>
    //                {Title}<span class='caret'></span>
    //            </button>
    //            <ul class='dropdown-menu'>";

    //        output.TagName = "div";
    //        output.Attributes.SetAttribute("role", "dialog");
    //        output.Attributes.SetAttribute("id", Id);
    //        output.Attributes.SetAttribute("aria-labelledby", $"{context.UniqueId}Label");
    //        output.Attributes.SetAttribute("tabindex", "-1");
    //        var classNames = "modal fade";
    //        if (output.Attributes.ContainsName("class"))
    //        {
    //            classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
    //        }
    //        output.Attributes.SetAttribute("class", classNames);
    //        output.Content.AppendHtml(template);
    //        if (modalContext.Body != null)
    //        {
    //            output.Content.AppendHtml(modalContext.Body);
    //        }
    //        output.Content.AppendHtml("</div>");
    //        if (modalContext.Footer != null)
    //        {
    //            output.Content.AppendHtml("<div class='modal-footer'>");
    //            output.Content.AppendHtml(modalContext.Footer);
    //            output.Content.AppendHtml("</div>");
    //        }

    //        output.Content.AppendHtml("</div></div>");
    //    }
    //}

    //internal class ButtonGroupContext
    //{
    //    public List<IHtmlContent> Links { get; set; }
    //    public IHtmlContent Footer { get; set; }
    //}

    //[HtmlTargetElement("btn-group-link", ParentTag = "btn-group")]
    //public class ButtonGroupLinkTagHelper : TagHelper
    //{
    //    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        output.Content.SetContent(@"<li class='divider'>")
    //    }
    //}

    //[HtmlTargetElement("btn-group-link", ParentTag = "btn-group")]
    //public class ButtonGroupDividerTagHelper : TagHelper
    //{
    //    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        var childContent = await output.GetChildContentAsync();
    //        var modalContext = (ModalContext)context.Items[typeof(ModalTagHelper)];
    //        modalContext.Body = childContent;
    //        output.SuppressOutput();
    //    }
    //}
}
