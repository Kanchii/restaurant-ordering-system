using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantOrderingSystem.Models.Entities.Category
{
    public class UpdateCategoryRequest
    {
        [JsonPropertyName("name"), Required]
        public string Name { get; set; } = string.Empty;
    }
}
