namespace DZ12.Models
{
    public class SeedData
    {
        public static void Initialize(ApplicationContext context)
        {
            if (context.Autos.Any())
                return;

            context.Autos.AddRange(
                new Auto { Model = "Toyota Camry", Type = "Седан", Year = 2018 },
                new Auto { Model = "BMW X5", Type = "Кроссовер", Year = 2020 },
                new Auto { Model = "Audi A4", Type = "Седан", Year = 2019 }
            );

            context.SaveChanges();
        }
    }
}
