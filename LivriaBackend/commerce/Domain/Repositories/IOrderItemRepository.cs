using LivriaBackend.commerce.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Repositories
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetByIdAsync(int id);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);

    }
}