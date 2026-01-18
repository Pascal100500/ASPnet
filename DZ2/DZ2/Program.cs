using System.Text;
using Microsoft.AspNetCore.Hosting.Server.Features;

var builder = WebApplication.CreateBuilder(args);
//var services = builder.Services;
//builder.Services.AddTransient<IMathFunctions, MathBuiltInFunctions>();
builder.Services.AddTransient<IMathFunctions, TaylorFunctions>();
var app = builder.Build();

app.MapGet("/", () => "Введите число аргумента для синуса, которое хотите посчитать, в адресную строку /hello?x=Число, \n" +
"к примеру https://localhost:7128/hello?x=1\n\n" +
"Для работы с встроенными математическими функциями раскоментируйте в коде строку:\n" +
"builder.Services.AddTransient<IMathFunctions, MathBuiltInFunctions>();\n" +
"для рассчета через ряд Тейлора, используйте строку:\n" +
"builder.Services.AddTransient<IMathFunctions, TaylorFunctions>();");


app.MapGet("/hello", (HttpContext context, IMathFunctions math) =>
{
    if (!context.Request.Query.ContainsKey("x"))
        return Results.Text(
            "Введите параметр x.\nПример: /hello?x=1",
            "text/plain; charset=utf-8"
        );

    if (!double.TryParse(context.Request.Query["x"], out double x))
        return Results.Text(
            "Параметр x должен быть числом.",
            "text/plain; charset=utf-8"
        );

    double result = math.Sin(x);
    return Results.Text(
        $"sin({x}) = {result}",
        "text/plain; charset=utf-8"
    );
});

//********************* 
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.ContentType = "text/html; charset=utf-8";
        await context.HttpContext.Response.WriteAsync(
            "<h1>Вы ввели неверный адрес</h1>" +
            "<a href='/'>Перейти на главную</a>"
        );
    }
});

app.Run();

/*
app.Lifetime.ApplicationStarted.Register(() =>
{
    var addresses = app.Services
        .GetRequiredService<Microsoft.AspNetCore.Hosting.Server.IServer>()
        .Features
        .Get<IServerAddressesFeature>()
        ?.Addresses;

    Console.WriteLine("СЕРВЕР СЛУШАЕТ АДРЕСА:");
    if (addresses != null)
    {
        foreach (var address in addresses)
        {
            Console.WriteLine(address);
        }
    }
});

Console.WriteLine("SERVER STARTING...");
*/

//************************************************
//Старый вариант запуска с Metanit через app.Run
//************************************************

/*
app.Run(async context =>
{
    var math = context.RequestServices.GetRequiredService<IMathFunctions>();

    if (!context.Request.Query.ContainsKey("x"))
    {
        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync(
            "Введите значение аргумента синуса в адресной строке.\n" +
            "пример https://localhost:7128/hello?x=1"
            );
        return;
    }
    if (!double.TryParse(context.Request.Query["x"], out double x))
    {
        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync("Значение аргумента должно быть числом.");
        return;
    }
    double result = math.Sin(x);
    context.Response.ContentType = "text/plain; charset=utf-8";
    await context.Response.WriteAsync($"sin({x}) = {result}");
});

app.Run();
*/
//************************************************



public interface IMathFunctions
{
    double Sin(double x);
}

public class MathBuiltInFunctions : IMathFunctions
{
    public double Sin(double x)
    {
        return Math.Sin(x);
    }
}

public class TaylorFunctions : IMathFunctions
{
    public double Sin(double x)
    {
        double result = 0.0;
        int znak = 1;
        int terms = 10;
        for (int i = 0; i < terms; i++)
        {
            int stepen = 2 * i + 1;
            double term = znak * Math.Pow(x, stepen) / Factorial(stepen);
            result += term;
            znak *= -1;
        }
        return result;
    }
    public double Factorial(int n)
    {
        double result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }
}




