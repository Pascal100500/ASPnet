using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ4.Pages
{
    public class AboutModel : PageModel
    {
        public string Name { get; set; }
        public string Group { get; set; }

        public void OnGet()
        {
            Name = "Максим";
            Group = "БВ425";
        }
    }
}
