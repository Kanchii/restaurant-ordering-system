using System.Text.Json.Serialization;

namespace RestaurantOrderingSystem.Models.Entities.Product
{
    public class CreateProductRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
