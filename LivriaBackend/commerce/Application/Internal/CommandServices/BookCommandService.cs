using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.CommandServices
{
    public class BookCommandService : IBookCommandService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookCommandService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle(CreateBookCommand command)
        {
            var book = new Book(
                command.Title,
                command.Description,
                command.Author,
                command.Price,
                command.Stock,
                command.Cover,
                command.Genre,
                command.Language
            );

            await _bookRepository.AddAsync(book);
            await _unitOfWork.CompleteAsync(); 

            return book; 
        }
    }
}