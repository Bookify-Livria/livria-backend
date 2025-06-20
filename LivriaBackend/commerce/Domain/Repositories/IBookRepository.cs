using LivriaBackend.commerce.Domain.Model.Aggregates;

namespace LivriaBackend.commerce.Domain.Repositories;
public interface IBookRepository
{
    Task<IEnumerable<Book>> ListAsync();
    Task<Book?> FindByIdAsync(int id);
    Task AddAsync(Book book);
    void Update(Book book);
    void Remove(Book book);
}