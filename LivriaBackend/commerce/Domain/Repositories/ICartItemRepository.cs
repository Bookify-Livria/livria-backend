using LivriaBackend.commerce.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userClientId);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(CartItem cartItem);
        Task<CartItem> FindByBookAndUserAsync(int bookId, int userClientId); 
    }
}