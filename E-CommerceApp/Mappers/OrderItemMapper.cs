

using E_CommerceApp.Dtos.Requests.OrderItem;
using E_CommerceApp.Dtos.Responses.OrderItem;
using E_CommerceApp.Models;

namespace E_CommerceApp.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemDto OrderItemToOrderItemDto(this OrderItem OrderItem)
        {
            return new OrderItemDto
            {
                ProductId = OrderItem.ProductId,
                Product = OrderItem.Product.ProductToProductDto(),
                OrderId = OrderItem.OrderId,
                Quantity = OrderItem.Quantity,
                Price = OrderItem.Price,
            };
        }

        public static OrderItem UpdateOrderIemDtoToOrderItem(this UpdateOrderItemDto updateOrderItemDto, int orderItemId,decimal orderItemPrice)
        {
            return new OrderItem
            {
                OrderItemId = orderItemId,
                ProductId = updateOrderItemDto.ProductId,
                OrderId = updateOrderItemDto.OrderId,
                Quantity = updateOrderItemDto.Quantity,
                Price = orderItemPrice,

            };
        }

        public static OrderItem CreateOrderItemDtoToOrderItem(this CreateOrderItemDto createOrderItem ,int ordeId, decimal orderItemPrice)
        {
            return new OrderItem
            {
                OrderId = ordeId,
                ProductId = createOrderItem.ProductId,
                Quantity = createOrderItem.Quantity,
                Price = orderItemPrice
            };
        }
    }
}
