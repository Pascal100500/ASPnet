using Autofac;
using Autofac.Extensions.DependencyInjection;

//Использованы пакеты:
// Autofac 9.0.0
// Autofac.Extensions.DependencyInjection 10.0.0

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory()
);


builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterType<CityService>()
             .As<ICityService>()
             .InstancePerDependency();

    container.RegisterType<FileCityInfoService>()
            .As<ICityInfoService>()
            .SingleInstance();
});

var app = builder.Build();

app.UseStaticFiles(); // для отправки картинок

app.MapGet("/", () =>
    "Доступные маршруты:\n" +
    "https://localhost:7206/city/Москва/coords\n" +
    "https://localhost:7206/city/Берлин/coords\n" +
    "https://localhost:7206/city/Москва/info\n" +
    "https://localhost:7206/city/Берлин/info\n" +
    "https://localhost:7206/city/Москва/monuments\n" +
    "https://localhost:7206/city/Берлин/monuments\n"
);

app.MapGet("/city/{name}/coords", (string name, ICityService service) =>
{
    var coords = service.GetCoordinates(name);

    if (coords == null)
    {
        string html_notF =
            "<h2>Город не найден</h2>" +
            "<br><a href='/'>На главную</a>";

        return Results.Content(html_notF, "text/html; charset=utf-8");
    }

    string html =
        $"<h1>Город: {name}</h1>" +
        $"<p><b>Широта:</b> {coords.Latitude}</p>" +
        $"<p><b>Долгота:</b> {coords.Longitude}</p>" +
        "<br><a href='/'>На главную</a>";

    return Results.Content(html, "text/html; charset=utf-8");
});

app.MapGet("/city/{name}/info", (string name, ICityInfoService service) =>
{
    var weather = service.GetWeather(name);
    var area = service.GetArea(name);
    var population = service.GetPopulation(name);


    if (weather == null && area == null && population == null)
    {
        string html_notF =
       "<h2>Информация о городе не найдена</h2>" +
       "<a href='/'>На главную</a>";
            return Results.Content(html_notF, "text/html; charset=utf-8");
    }

    string html =
        $"<h1>Город: {name}</h1>" +
        $"<p><b>Погода:</b> {weather}</p>" +
        $"<p><b>Площадь:</b> {area:N1} км²</p>" +
        $"<p><b>Население:</b> {population:N0} человек</p>" +
        "<br><a href='/'>На главную</a>";

    return Results.Content(html, "text/html; charset=utf-8");
});

app.MapGet("/city/{name}/monuments", (string name, IWebHostEnvironment env) =>
{
    string cityFolder = name.ToLowerInvariant();

    string monumentsPath = Path.Combine(
        env.WebRootPath,
        "monuments",
        cityFolder
    );

    if (!Directory.Exists(monumentsPath))
        return Results.Text(
            "Памятники для данного города не найдены",
            "text/plain; charset=utf-8"
        );

    var files = Directory.GetFiles(monumentsPath);

    if (files.Length == 0)
        return Results.Text(
            "В папке нет изображений",
            "text/plain; charset=utf-8"
        );

    var html =
        $"<h1>Архитектурные памятники города {name}</h1>";

    foreach (var file in files)
    {
        var fileName = Path.GetFileName(file);
        html +=
            $"<img src='/monuments/{cityFolder}/{fileName}' " +
            $"width='300' style='margin:10px;' />";
    }

    html += "<br><br><a href='/'>На главную</a>";

    return Results.Content(html, "text/html; charset=utf-8");
});

app.Run();

public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public interface ICityService
{
    Coordinates GetCoordinates(string city);
}

public class CityService : ICityService
{
    public Coordinates GetCoordinates(string city)
    {
        if (string.Equals(city, "Москва", StringComparison.OrdinalIgnoreCase))
        {
            return new Coordinates
            {
                Latitude = 55.5587,
                Longitude = 37.3788
            };
        }

        if (string.Equals(city, "Берлин", StringComparison.OrdinalIgnoreCase))
        {
            return new Coordinates
            {
                Latitude = 52.5400,
                Longitude = 13.4105
            };
        }

        return null;
    }
}

public interface ICityInfoService
{
    string GetWeather(string city);
    double? GetArea(string city);
    long? GetPopulation(string city);
}

public class FileCityInfoService : ICityInfoService
{
    private readonly string _dataPath;

    public FileCityInfoService(IWebHostEnvironment env)
    {
        _dataPath = Path.Combine(env.ContentRootPath, "Data");
    }

    private Dictionary<string, string> ReadCityFile(string city)
    {
        string fileName = city.ToLowerInvariant() + ".txt";
        string fullPath = Path.Combine(_dataPath, fileName);

        if (!File.Exists(fullPath))
            return null;

        return File.ReadAllLines(fullPath)
            .Select(line => line.Split('=', 2))
            .Where(parts => parts.Length == 2)
            .ToDictionary(
                parts => parts[0],
                parts => parts[1]
            );
    }

    public string GetWeather(string city)
    {
        var data = ReadCityFile(city);
        return data != null && data.ContainsKey("Weather")
            ? data["Weather"]
            : null;
    }

    public double? GetArea(string city)
    {
        var data = ReadCityFile(city);

        if (data == null || !data.ContainsKey("Area"))
            return null;

        if (double.TryParse(
            data["Area"],
            System.Globalization.CultureInfo.InvariantCulture,
            out double area))
            return area;

        return null;
    }

    public long? GetPopulation(string city)
    {
        var data = ReadCityFile(city);

        if (data == null || !data.ContainsKey("Population"))
            return null;

        if (long.TryParse(data["Population"], out long population))
            return population;

        return null;
    }
}






/*
var builder = WebApplication.CreateBuilder(args);

// 🔹 ВАЖНО: меняем ServiceProvider
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory()
);

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // Регистрация сервисов будет здесь
});

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterType<TestService>()
             .As<ITestService>()
             .InstancePerDependency();
});

var app = builder.Build();

app.MapGet("/", (ITestService service) =>
{
    return service.GetMessage();
});

app.Run();





public interface ITestService
{
    string GetMessage();
}

public class TestService : ITestService
{
    public string GetMessage() => "Autofac работает!";
}
*/