using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DZ10.TagHelpers
{
    public class ImageTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var imageName = output.GetChildContentAsync().Result.GetContent();
            var imagePath = $"/images/{imageName}.jpg";
            var physicalPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                $"{imageName}.jpg"
            );

            if (!File.Exists(physicalPath))
            {
                // При отсутствии картинки выведется текст с предупреждением
                output.TagName = "span";
                output.Content.SetContent("Картинка не загрузилась");
                return;
            }

            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("src", imagePath);
            output.Attributes.SetAttribute("alt", imageName);
        }
    }
}
