using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Dtos.Responses.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string Stock { get; set; } = string.Empty ;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
