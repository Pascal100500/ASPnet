using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Nodes;

namespace DZ4.Pages
{
    public class FunctionsModel : PageModel
    {
        public double SinValue { get; set; }
        public double CosValue { get; set; }
        public int FactorialValue { get; set; }
        public void OnGet()
        {
            SinValue = Math.Sin(1);
            CosValue = Math.Cos(1);
            FactorialValue = Factorial(7);
        }

        int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            return result;
        }
    }
}
