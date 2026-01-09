var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// дополнительный параметр
app.MapGet("/hello", (string? name) => $"Привет {name} !");
// проверка по https://localhost:7056/hello?name=Вася

app.Run();