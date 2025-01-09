using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceApp.Dtos.Requests.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "Product Name must be less than 150 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Price { get; set; } = decimal.Zero;
        [Required]
        [MaxLength(150, ErrorMessage = "Product Name must be less than 150 characters")]
        public string Stock { get; set; } = string.Empty ;
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
