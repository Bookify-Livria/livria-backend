using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<Review> GetByIdAsync(int id)
        {
            return await this.Context.Reviews
                .Include(r => r.Book)
                .Include(r => r.UserClient) 
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public new async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await this.Context.Reviews
                .Include(r => r.Book)
                .Include(r => r.UserClient) 
                .ToListAsync();
        }

        public async Task AddAsync(Review review)
        {
            await this.Context.Reviews.AddAsync(review);
        }
    }
}