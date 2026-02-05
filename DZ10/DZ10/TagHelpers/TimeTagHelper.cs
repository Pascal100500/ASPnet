using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DZ10.TagHelpers
{
    public class TimeTagHelper : TagHelper
    {
        // offset от GMT
        public int Offset { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            var time = DateTime.UtcNow.AddHours(Offset)
                                      .ToString("HH:mm:ss");

            output.Content.SetContent(time);
        }
    }
}
