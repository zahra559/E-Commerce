using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Models;

namespace E_CommerceApp.Interfaces
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<List<OrderItem>> GetOrderItems(GetOrderItemsDto criteria);
    }
}
