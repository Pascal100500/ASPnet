using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ8.Pages
{
    public class WeatherModel : PageModel
    {
        public string Title { get; private set; } = "";
        public void OnGet()
        {
            ViewData["Cities"] = new List<string>
            {
                "Москва", "Лондон", "Париж"
            };

            Title = "Погода в городах";
        }
    }
}
