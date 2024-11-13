using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public sealed class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context) 
        {
            _context = context;
        }

        ///<returns>
        /// Return the user with the maximum total amount of items delivered in 2003 or null
        /// </returns>
        public async Task<User?> GetUser()
        {
            return await _context.Users
                .OrderByDescending(user =>
                    user.Orders.Where(order => order.CreatedAt.Year == 2003).Sum(o => o.Quantity))
                .FirstOrDefaultAsync();
        }

        ///<returns>
        /// Return collection of users who have paid orders in 2010 or an empty collection
        /// </returns>
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Where(user =>
                    user.Orders.Any(order => 
                        order.Status == OrderStatus.Paid
                        && order.CreatedAt.Year == 2010))
                .ToListAsync();
        }
    }
}
