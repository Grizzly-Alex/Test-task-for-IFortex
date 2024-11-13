using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public sealed class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context) 
        {
            _context = context;
        }

        ///<returns>
        /// Return the most new order that contains more than one item or null.
        /// </returns>
        public async Task<Order?> GetOrder()
        {
            return await _context.Orders.OrderByDescending(order => order.CreatedAt)
                .FirstOrDefaultAsync(order => order.Quantity > 1);
        }

        ///<returns>
        /// Return collection of orders from active users, sorted by creation date or an empty collection.
        /// </returns>
        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Where(order => order.User.Status == UserStatus.Active)
                .OrderBy(order => order.CreatedAt)
                .ToListAsync();
        }
    }
}
