var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// регистрируем сервис времени
builder.Services.AddTransient<ICityTimeService, CityTimeService>();
builder.Services.AddTransient<IWeatherService, WeatherService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();

public interface ICityTimeService
{
    string GetTime(string city);
}

public class CityTimeService : ICityTimeService
{
    public string GetTime(string city)
    {
        return city switch
        {
            "Москва" => DateTime.UtcNow.AddHours(3).ToString("HH:mm:ss"),
            "Лондон" => DateTime.UtcNow.ToString("HH:mm:ss"),
            "Токио" => DateTime.UtcNow.AddHours(9).ToString("HH:mm:ss"),
            "Нью-Йорк" => DateTime.UtcNow.AddHours(-5).ToString("HH:mm:ss"),
            _ => "Неизвестный город"
        };
    }
}

public interface IWeatherService
{
    string GetWeather(string city);
}

public class WeatherService : IWeatherService
{
    public string GetWeather(string city)
    {
        return city switch
        {
            "Москва" => "+18°C, облачно",
            "Лондон" => "+15°C, дождь",
            "Париж" => "+17°C, ясно",
            _ => "Нет данных"
        };
    }
}