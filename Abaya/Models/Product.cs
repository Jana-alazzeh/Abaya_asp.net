using System.ComponentModel.DataAnnotations;

namespace Abaya.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } // صورة الوجه

        public string? ImageUrl_Back { get; set; } // صورة الظهر (اختياري)
    }
}