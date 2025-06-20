using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Infrastructure.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<CartItem> GetByIdAsync(int id)
        {
            return await this.Context.CartItems
                .Include(ci => ci.Book)
                .Include(ci => ci.UserClient)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userClientId)
        {
            return await this.Context.CartItems
                .Where(ci => ci.UserClientId == userClientId)
                .Include(ci => ci.Book)
                .Include(ci => ci.UserClient)
                .ToListAsync();
        }

        public async Task AddAsync(CartItem cartItem)
        {
            await this.Context.CartItems.AddAsync(cartItem);
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            this.Context.Entry(cartItem).State = EntityState.Modified;
            await Task.CompletedTask; 
        }

        public async Task DeleteAsync(CartItem cartItem)
        {
            this.Context.CartItems.Remove(cartItem);
            await Task.CompletedTask; 
        }

        public async Task<CartItem> FindByBookAndUserAsync(int bookId, int userClientId)
        {
            return await this.Context.CartItems
                .Where(ci => ci.BookId == bookId && ci.UserClientId == userClientId)
                .FirstOrDefaultAsync();
        }
    }
}