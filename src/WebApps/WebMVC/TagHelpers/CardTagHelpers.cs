using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.TagHelpers
{
    public class CardContext
    {
        public IHtmlContent Body { get; set; }
        public IHtmlContent Footer { get; set; }
    }

    /// <summary>
    /// A Bootstrap modal dialog
    /// </summary>
    [RestrictChildren("card-body", "card-footer")]
    public class CardTagHelper : TagHelper
    {
        /// <summary>
        /// The title of the modal
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The title of the modal
        /// </summary>
        public string Style { get; set; } = "default";

        /// <summary>
        /// The Id of the modal
        /// </summary>
        public string Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var cardContext = new CardContext();
            context.Items.Add(typeof(CardTagHelper), cardContext);

            await output.GetChildContentAsync();

            var template =
            $@"<div class='card card-{Style}'>
                <div class='card-header'>
                    <h3 class='card-title'>{Title}</h3>
                </div>
                <div class='card-body'>";
            
            output.Content.AppendHtml(template);

            if (cardContext.Body != null)
            {
                output.Content.AppendHtml(cardContext.Body);
            }
            output.Content.AppendHtml("</div>");
            if (cardContext.Footer != null)
            {
                output.Content.AppendHtml("<div class='card-footer'>");
                output.Content.AppendHtml(cardContext.Footer);
                output.Content.AppendHtml("</div>");
            }

            output.Content.AppendHtml("</div>");
        }
    }

    [HtmlTargetElement("card-body", ParentTag = "card")]
    public class CardBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var cardContext = (CardContext)context.Items[typeof(CardTagHelper)];
            cardContext.Body = childContent;
            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("card-footer", ParentTag = "card")]
    public class CardFooterTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var cardContext = (CardContext)context.Items[typeof(CardTagHelper)];
            cardContext.Footer = childContent;
            output.SuppressOutput();
        }
    }
}
