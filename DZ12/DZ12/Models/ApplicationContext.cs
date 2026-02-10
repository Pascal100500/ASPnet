
using Microsoft.EntityFrameworkCore;


namespace DZ12.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Auto> Autos { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
