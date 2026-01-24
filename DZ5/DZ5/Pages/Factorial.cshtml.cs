using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ5.Pages
{
    public class FactorialModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int N { get; set; }

        public long Result { get; set; }
        public string Message { get; set; } = "";

        public void OnGet()
        {
            Message = "Данные получены через GET";

            if (N > 0)
                Result = CalculateFactorial(N);
        }

        public void OnPost()
        {
            Message = "Данные получены через POST";
            Result = CalculateFactorial(N);
        }

        private long CalculateFactorial(int n)
        {
            if (n < 0) return 0;

            long res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;

            return res;
        }
    }
}
