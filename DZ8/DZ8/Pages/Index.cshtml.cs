using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ8.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // ViewData — список городов
            ViewData["Cities"] = new List<string>
            {
                "Москва",
                "Лондон",
                "Токио",
                "Нью-Йорк"
            };
        }
    }
}
