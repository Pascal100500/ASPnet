using System.ComponentModel.DataAnnotations;

namespace DZ12.Models
{
    public class Auto
    {
        [Key]
        public int Id { get; set; }
        public String? Model { get; set; }
        public string? Type { get; set; }

        public int Year { get; set; }
    }
}
