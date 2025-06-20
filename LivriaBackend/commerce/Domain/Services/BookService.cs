using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.Shared.Domain.Repositories;
namespace LivriaBackend.commerce.Domain.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Book>> ListAsync() => await _bookRepository.ListAsync();

    public async Task<Book?> GetByIdAsync(int id) => await _bookRepository.FindByIdAsync(id);

    public async Task<Book> CreateAsync(Book book)
    {
        await _bookRepository.AddAsync(book);
        await _unitOfWork.CompleteAsync();
        return book;
    }

    public async Task<Book?> UpdateAsync(int id, Book updatedBook)
    {
        var existing = await _bookRepository.FindByIdAsync(id);
        if (existing == null) return null;

        existing.Title = updatedBook.Title;
        existing.Genre = updatedBook.Genre;
        existing.Price = updatedBook.Price;
        existing.PublishDate = updatedBook.PublishDate;
        existing.Format = updatedBook.Format;
        existing.Description = updatedBook.Description;
        existing.Cover = updatedBook.Cover;
        existing.Language = updatedBook.Language;
        existing.Author = updatedBook.Author;

        _bookRepository.Update(existing);
        await _unitOfWork.CompleteAsync();

        return existing;
    }

    public async Task<Book?> DeleteAsync(int id)
    {
        var existing = await _bookRepository.FindByIdAsync(id);
        if (existing == null) return null;

        _bookRepository.Remove(existing);
        await _unitOfWork.CompleteAsync();
        return existing;
    }
}