using System.ComponentModel.DataAnnotations;

namespace DZ11.Models
{
    public class Game
    {
        [Required(ErrorMessage = "Название игры обязательно")]
        public string Name { get; set; } = "";

        [Range(1, 10000, ErrorMessage = "Цена должна быть больше 0")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Описание обязательно")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Выберите жанр")]
        public string Genre { get; set; } = "";

        public bool IsPaid { get; set; }

        [Range(3, 18, ErrorMessage = "Возраст от 3 до 18")]
        public int AgeLimit { get; set; }

        [Range(0, 10)]
        public int Rating { get; set; }
    }
}
