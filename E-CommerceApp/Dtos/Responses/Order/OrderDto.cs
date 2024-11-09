using E_CommerceApp.Dtos.Responses.OrderItem;
using E_CommerceApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceApp.Dtos.Responses.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderSummary { get; set; } = string.Empty;
        public bool CheckedOut { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } 
    }
}
