using DZ11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ11.Pages
{
    public class AddGameModel : PageModel
    {
        [BindProperty]
        public Game Game { get; set; } = new();

        public string Message { get; private set; } = "Добавление игры";

        public List<string> Genres { get; } = new()
        {
            "RPG",
            "Action",
            "Strategy"
        };

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Message = "Ошибка ввода данных";
                return;
            }

            Message = $"Добавлена игра: {Game.Name} ({Game.Genre})";
        }
    }
}
