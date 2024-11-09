using E_CommerceApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceApp.Dtos.Requests.OrderItem
{
    public class UpdateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
