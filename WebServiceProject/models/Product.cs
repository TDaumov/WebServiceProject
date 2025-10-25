using System.ComponentModel.DataAnnotations;

namespace WebServiceProject.Models
{
    public class Product
    {
        [Key] // Указывает, что это первичный ключ
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно для заполнения")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно содержать от 2 до 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, 100000, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Категория обязательна")]
        public string Category { get; set; } = string.Empty;
    }
}