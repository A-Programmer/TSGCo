
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Project.Auth.TagHelpers
{
    [HtmlTargetElement("i", Attributes = CheckMarkValueAttributeName)]
    public class KSCheckMarkTagHelper : TagHelper
    {
        private const string CheckMarkValueAttributeName = "ks-value";
        private const string CheckMarkTrueTextValueAttributeName = "ks-true-text";
        private const string CheckMarkFalseValueAttributeName = "ks-false-text";

        [HtmlAttributeName(CheckMarkValueAttributeName)]
        public bool CheckMarkValue { get; set; }

        [HtmlAttributeName(CheckMarkTrueTextValueAttributeName)]
        public string CheckMarkTrueTextValue { get; set; }

        [HtmlAttributeName(CheckMarkFalseValueAttributeName)]
        public string CheckMarkFalseTextValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // string result = "";
            string classValue = "";
            string textValue = " ";
            if (CheckMarkValue)
            {
                classValue = "fa fa-check text-success";
                textValue = CheckMarkTrueTextValue;
            }
            else
            {
                classValue = "fa fa-times text-danger";
                textValue = CheckMarkFalseTextValue;
            }

            output.Attributes.SetAttribute("class", classValue);
            output.Content.AppendHtml(textValue);

            base.Process(context, output);
        }
    }
}