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
    public class DeleteModel : PageModel
    {
        private readonly DZ12.Models.ApplicationContext _context;

        public DeleteModel(DZ12.Models.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Auto Auto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FirstOrDefaultAsync(m => m.Id == id);

            if (auto == null)
            {
                return NotFound();
            }
            else
            {
                Auto = auto;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FindAsync(id);
            if (auto != null)
            {
                Auto = auto;
                _context.Autos.Remove(Auto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
