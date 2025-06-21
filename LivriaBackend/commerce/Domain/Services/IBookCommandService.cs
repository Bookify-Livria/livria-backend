using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Model.Services
{
    public interface IBookCommandService
    {
        Task<Book> Handle(CreateBookCommand command);
        
        /// <summary>
        /// Maneja el comando para actualizar el stock de un libro existente.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del libro y el nuevo stock.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es el objeto <see cref="Book"/> actualizado, o <c>null</c> si el libro no se encuentra.
        /// </returns>
        Task<Book?> Handle(UpdateBookStockCommand command);
    }
}