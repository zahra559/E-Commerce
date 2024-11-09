using E_CommerceApp.Dtos.Responses.Order;
using E_CommerceApp.Dtos.Responses.Product;
using E_CommerceApp.Models;

namespace E_CommerceApp.Dtos.Responses.OrderItem
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
