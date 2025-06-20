using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<Book> GetByIdAsync(int id)
        {
            return await this.Context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public new async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await this.Context.Books
                .Include(b => b.Reviews)
                .ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await this.Context.Books.AddAsync(book);
        }

        public async Task UpdateAsync(Book book) 
        {
           
            this.Context.Entry(book).State = EntityState.Modified;
            await Task.CompletedTask; 
        }
    }
}