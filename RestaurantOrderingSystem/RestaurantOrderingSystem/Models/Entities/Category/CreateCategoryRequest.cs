using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantOrderingSystem.Models.Entities.Category
{
    public class CreateCategoryRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
