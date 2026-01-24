using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DZ5.Pages
{
    public class FibonacciModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int N { get; set; }
        public long Result { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            Result = CalculateFibonacci(N);
        }

        private long CalculateFibonacci(int n)
        {
            if (n <= 1) return n;

            long a = 0, b = 1;
            for (int i = 2; i <= n; i++)
            {
                long temp = a + b;
                a = b;
                b = temp;
            }
            return b;
        }
    }
}
