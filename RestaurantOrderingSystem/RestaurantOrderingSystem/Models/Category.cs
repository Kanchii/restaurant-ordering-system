using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
