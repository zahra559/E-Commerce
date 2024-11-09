using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Models;

namespace E_CommerceApp.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrders(GetOrdersDto criteria);
        Task<Order?> GetOrderByIdAsync(int orderId);
    }
}
