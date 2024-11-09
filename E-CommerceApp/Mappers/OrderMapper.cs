using E_CommerceApp.Dtos.Requests.OrderItem;
using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Dtos.Responses.Order;
using E_CommerceApp.Models;

namespace E_CommerceApp.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto OrderToOrderDto(this Order Order)
        {
            return new OrderDto
            {
                OrderId = Order.OrderId,
                ApplicantName = Order.Applicant?.UserName,
                OrderDate = Order.OrderDate,
                OrderSummary = Order.OrderSummary,
                CheckedOut = Order.CheckedOut,
                TotalAmount = Order.TotalAmount,
                OrderItems = Order.OrderItems.Select(o => o.OrderItemToOrderItemDto()).ToList(),
            };
        }


    }
}
