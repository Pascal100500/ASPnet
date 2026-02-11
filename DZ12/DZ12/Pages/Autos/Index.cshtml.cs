using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DZ12.Models;

namespace DZ12.Pages.Autos
{
    public class IndexModel : PageModel
    {
        private readonly DZ12.Models.ApplicationContext _context;

        public IndexModel(DZ12.Models.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Auto> Auto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Auto = await _context.Autos.ToListAsync();
        }
    }
}
