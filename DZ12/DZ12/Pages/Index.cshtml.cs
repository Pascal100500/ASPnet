using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DZ12.Models;

namespace DZ12.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<IndexModel> _logger;
        public List<Auto> Autos { get; private set; } = new();

        // ≈динственный конструктор Ч внедр€ем и контекст, и логгер
        public IndexModel(ApplicationContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {
            _context.Database.EnsureCreated();
            if (!_context.Autos.Any())
            {
                SeedData.Initialize(_context);
            }
            Autos = _context.Autos.AsNoTracking().ToList();
        }
    }
}
