using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Repositories; 
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<Order> GetByIdAsync(int id)
        {
            return await this.Context.Orders
                .Include(o => o.Items) 
                
                .Include(o => o.UserClient) 
                .Include(o => o.Shipping)   
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userClientId)
        {
            return await this.Context.Orders
                .Where(o => o.UserClientId == userClientId)
                .Include(o => o.Items)
                .Include(o => o.UserClient)
                .Include(o => o.Shipping)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByCodeAsync(string code)
        {
            return await this.Context.Orders
                .Include(o => o.Items)
                .Include(o => o.UserClient)
                .Include(o => o.Shipping)
                .FirstOrDefaultAsync(o => o.Code == code);
        }

        public async Task AddAsync(Order order)
        {
            await this.Context.Orders.AddAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            this.Context.Entry(order).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Order order)
        {
            this.Context.Orders.Remove(order);
            await Task.CompletedTask;
        }
    }
}