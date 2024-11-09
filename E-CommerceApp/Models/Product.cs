using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceApp.Models
{
    [Table("Products")]
    [Index(nameof(Name), IsUnique = true)]

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Stock { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
