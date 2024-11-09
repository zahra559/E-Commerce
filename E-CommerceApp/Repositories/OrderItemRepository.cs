using E_CommerceApp.Data;
using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {

        public OrderItemRepository(ApplicationDBContext context) : base(context) { }

        public async Task<List<OrderItem>> GetOrderItems(GetOrderItemsDto criteria)
        {
            var orderItems = _context.OrderItems.AsQueryable();

            if (criteria.OrderId != null)
                orderItems = orderItems.Where(x => x.OrderId == criteria.OrderId);

            if (criteria.ProductId != null)
                orderItems = orderItems.Where(x => x.ProductId == criteria.ProductId);

            return await orderItems.ToListAsync();
        }


    }
}
