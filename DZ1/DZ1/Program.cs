var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Введите число рублей, которые хотите перевести в доллары в адресе /hello?num=Число, " +
"к примеру https://localhost:7179/hello?num=8100");

// дополнительный параметр
app.MapGet("/hello", (string? num) =>
{
    if (num == null)
        return "Введите параметр num в адресе: /hello?num=Число";

    if (!double.TryParse(num, out double rub))
        return "Вы ввели не число";

    double kurs = 81;
    double dollars = rub / kurs;

    return $"{num} рублей = {dollars} долларов";
});

app.Run();