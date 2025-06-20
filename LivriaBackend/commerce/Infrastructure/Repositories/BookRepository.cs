using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LivriaBackend.commerce.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> ListAsync() => await _context.Books.ToListAsync();

    public async Task<Book?> FindByIdAsync(int id) => await _context.Books.FindAsync(id);

    public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);

    public void Update(Book book) => _context.Books.Update(book);

    public void Remove(Book book) => _context.Books.Remove(book);
}