using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ6.Pages
{
    public record City(string Name, int Population);
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string Message { get; private set; } = "";

        private List<City> cities = new()
        {
            new City ("Москва", 14_000_000),
            new City ("Берлин", 3_000_000),
            new City ("Париж", 5_000_000),
            new City ("Рим", 12_000_000)
        };
        public List<City> DisplayedCities { get; private set; } = new();

        public void OnGet()
        {
            DisplayedCities = cities;
        }

        public void OnGetByCity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayedCities = cities;
                Message = "Показаны все города";
            }
            else
            {
                DisplayedCities = cities
                    .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (DisplayedCities.Count == 0)
                {
                    Message = $"Город с названием \"{name}\" не найден";
                }
                else
                {
                    Message = $"Найдено городов: {DisplayedCities.Count}";
                }
            }
        }
        public void OnGetByPopulation(int population)
        {
            if (population == 0)
            {
                DisplayedCities = cities;
                Message = "Показаны все города";
            }
            else if (population < 0)
            {
                DisplayedCities = new List<City>();
                Message = "Население не может быть меньше 0";
            }
            else
            {
                DisplayedCities = cities
                    .Where(c => c.Population == population)
                    .ToList();

                if (DisplayedCities.Count == 0)
                {
                    Message = "Город с таким населением не найден";
                }
                else
                {
                    Message = $"Найдено городов: {DisplayedCities.Count}";
                }
            }
        }

    }
}
