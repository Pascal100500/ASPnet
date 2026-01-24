using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ4.Pages
{
    public class ConverterModel : PageModel
    {
        public double Dollars { get; set;}
        public double Rubles { get; set;}
        public void OnGet()
        {
            double rate = 72;
            Dollars = 1;
            Rubles = Dollars * rate;
        }
    }
}
