using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new();
    }
}
