using Microsoft.EntityFrameworkCore;
using DZ12.Models;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
// string? connection = builder.Configuration.GetConnectionString("MsDocker");
//string? con1 = builder.Configuration.GetConnectionString("SqlExpress");
//string? con2 = builder.Configuration.GetConnectionString("SQLite");
//string? con3 = builder.Configuration.GetConnectionString("Postgres");

//string? Db = "Postgres";

// добавляем контекст ApplicationContext в качестве сервиса в приложение
/*
 * switch (Db) 
{
    case "Postgres": builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(con3));
        break;
    case "SQLite": builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(con2));
        break;
    case "SqlExpress": builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(con3));
        break;
}
*/

// Мое подключения с PostgreSQL
string? connection = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(connection)
);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();