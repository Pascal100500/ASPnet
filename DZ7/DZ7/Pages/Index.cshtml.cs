using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace DZ7.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string FileContent { get; set; } = "";

        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult OnGet(string? name, string? mode)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                FileContent = "Укажите имя автора через ?name=Пушкин";
                return Page();
            }
            string filePath = Path.Combine(
               _env.ContentRootPath,
               "Data",
               $"{name}.txt"
            );

            // Перенаправление на Яндекс поиск, если файл не найден
            if (!System.IO.File.Exists(filePath))
            {
                string encodedName = WebUtility.UrlEncode(name);
                return Redirect($"https://yandex.ru/search/?text={encodedName}");
            }

            //JSON
            /*
             JSON — это формат передачи данных. Программы, которые будут читать JSON, поймут, 
             что \n — это новая строка. Браузер же просто отображает «сырой» текст. 
             Поэтому в окне браузера остаются спецсимволы, такие как "\n"
            */

            string text = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

            // Попытка сделать нормальный вывод JSON в браузере, если хотим красивый JSON без \r\n
            text = text.Replace("\r\n", "\n");

            // 1. JSON
            if (mode == "json")
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                return new JsonResult(new
                {
                    Author = name,
                    Text = text
                }, options);
            }

            // 2. Файл
            if (mode == "file")
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                return File(bytes, "text/plain", $"{name}.txt");
            }

            // 3. Обычный вывод
            FileContent = text;
            return Page();
        }
    }
}
