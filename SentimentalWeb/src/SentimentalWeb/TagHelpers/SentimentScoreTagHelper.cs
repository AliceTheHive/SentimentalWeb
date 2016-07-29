using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentalWeb.TagHelpers
{
    public class SentimentScoreTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Get child content (the content within the tags)
            var childContentRaw = (await output.GetChildContentAsync()).GetContent();

            //format as a percentage
            var childContent = Convert.ToDouble(childContentRaw);
            var display = Math.Round((childContent * 100), 0).ToString();

            //display output
            output.Content.SetContent(display);
        }
    }
}
