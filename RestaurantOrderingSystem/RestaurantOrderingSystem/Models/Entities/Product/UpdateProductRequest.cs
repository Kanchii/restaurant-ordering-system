using System.Text.Json.Serialization;

namespace RestaurantOrderingSystem.Models.Entities.Product
{
    public class UpdateProductRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }
    }
}
