using LivriaBackend.commerce.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userClientId);
        Task<Order> GetOrderByCodeAsync(string code); 
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}