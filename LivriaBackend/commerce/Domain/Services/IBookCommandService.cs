using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Model.Services
{
    public interface IBookCommandService
    {
        Task<Book> Handle(CreateBookCommand command);
    }
}