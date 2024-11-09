using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Dtos.Requests.OrderItem
{
    public class CreateOrderItemDto
    {
        public int? OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
