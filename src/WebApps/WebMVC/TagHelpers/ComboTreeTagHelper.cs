using Microsoft.AspNetCore.Razor.TagHelpers;
using WebMVC.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebMVC.TagHelpers
{
    [HtmlTargetElement("comboTree", Attributes = "comboTreeObjVar,controller,action")]
    public class ComboTreeTagHelper : TagHelper
    {
        [HtmlAttributeName("showItems")]
        public int[] ShowItemsWithIds { get; set; } = null;

        [HtmlAttributeName("selectedValues")]
        public int[] SelectedValues { get; set; } = null;

        [HtmlAttributeName("selectedValue")]
        public int? SelectedValue { get; set; } = null;

        [HtmlAttributeName("selectedValuesStrings")]
        public string[] SelectedValuesStrings { get; set; } = null;

        [HtmlAttributeName("selectedValueString")]
        public int? SelectedValueString { get; set; } = null;

        [HtmlAttributeName("isMultiselect")]
        public bool IsMultiselect { get; set; } = false;

        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; } = "Select groups";

        [HtmlAttributeName("appendToPlaceholder")]
        public bool AppendItemsToPlaceholder { get; set; } = true;

        [HtmlAttributeName("comboTreeObjVar")]
        public string ComboTreeObjVar { get; set; }

        [HtmlAttributeName("showMaxItems")]
        public int ShowMaxItems { get; set; } = 10;

        [HtmlAttributeName("small")]
        public bool Small { get; set; } = false;

        [HtmlAttributeName("selectionCallback")]
        public string SelectionCallback { get; set; }

        [HtmlAttributeName("action")]
        public string Action { get; set; }

        [HtmlAttributeName("controller")]
        public string Controller { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var input = $@"<div class='input-group'> 
                            <input type='text' id='{context.UniqueId}' placeholder='{Placeholder}' autocomplete='off' class='form-control {(Small ? "form-control-sm" : "")}'/>
                            </div>";
            var script = $@"<script src='/lib/comboTreePlugin/comboTreePlugin.js'></script>
                            <link href='/lib/comboTreePlugin/comboTreePlugin.css' rel='stylesheet'/>
        
                            <script>
                            var {ComboTreeObjVar};
                            $(function(){{

                                $.ajax({{
                                    url: '/{Controller}/{Action}',
                                    method:'POST',
                                    {(ShowItemsWithIds == null ? "" : $"'data': {{listIds: {JsonConvert.SerializeObject(ShowItemsWithIds)}}},")}
                                    success: function(data){{
                                        {ComboTreeObjVar} = $('#{context.UniqueId}').comboTree({{
                                            source: JSON.parse(data),
                                            isMultiple: {IsMultiselect.ToString().ToLower()},
                                            showMaxItems: {ShowMaxItems},
                                            appendToPlaceholder: {AppendItemsToPlaceholder.ToString().ToLower()},
                                            title: '{Placeholder}'
                                            {(SelectionCallback == null ? "" : $",selectCallback: {SelectionCallback}")}
                                            {(IsMultiselect && SelectedValues != null ? $",selectedValues: {JsonConvert.SerializeObject(SelectedValues)}" : "")}
                                            {(IsMultiselect && SelectedValuesStrings != null ? $",selectedValues: {JsonConvert.SerializeObject(SelectedValuesStrings)}" : "")}
                                            {(!IsMultiselect && SelectedValue != null ? $",selectedValue: {SelectedValue}" : "")}
                                            {(!IsMultiselect && SelectedValueString != null ? $",selectedValue: {SelectedValueString}" : "")}
                                        }});
                                    }}
                                }})
                                
                            }})</script>";
            output.Content.AppendHtml(input);
            output.Content.AppendHtml(script);

            if (output.Attributes.TryGetAttribute("comboTree", out TagHelperAttribute attribute))
                output.Attributes.Remove(attribute);
        }
    }
}
