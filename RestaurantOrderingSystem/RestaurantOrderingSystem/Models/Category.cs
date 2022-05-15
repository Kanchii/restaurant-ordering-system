using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestaurantOrderingSystem.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
