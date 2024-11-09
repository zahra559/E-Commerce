using E_CommerceApp.Data;
using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDBContext context) : base(context) { }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {

            return await _context.Orders.Include(x => x.OrderItems)
                .ThenInclude(p => p.Product)
                .Include(c => c.Applicant)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<List<Order>> GetOrders(GetOrdersDto criteria)
        {
            var orders = _context.Orders.AsQueryable();
            if (!string.IsNullOrWhiteSpace(criteria.ApplicantId))
                orders = orders.Where(x => x.ApplicantId.Equals(criteria.ApplicantId));

            if (criteria.OrderId != null)
                orders = orders.Where(x => x.OrderId == criteria.OrderId);

            if(criteria.CheckedOut != null)
                orders = orders.Where(x => x.CheckedOut == criteria.CheckedOut);

            return await orders.ToListAsync();
        }
    }
}
