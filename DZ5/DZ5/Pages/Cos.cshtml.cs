using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ5.Pages
{
    public class CosModel : PageModel
    {
        [BindProperty]
        public double X { get; set; }

        public double Result { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            Result = Math.Cos(X);
        }
    }
}
