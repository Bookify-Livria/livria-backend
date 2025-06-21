using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.CommandServices
{
    /// <summary>
    /// Implementa el servicio de comandos para la entidad <see cref="Book"/>.
    /// Procesa comandos relacionados con la gestión de libros, coordinando con el repositorio y la unidad de trabajo.
    /// </summary>
    public class BookCommandService : IBookCommandService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BookCommandService"/>.
        /// </summary>
        /// <param name="bookRepository">El repositorio de libros para operaciones de persistencia.</param>
        /// <param name="unitOfWork">La unidad de trabajo para gestionar transacciones y guardar cambios.</param>
        public BookCommandService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja el comando <see cref="CreateBookCommand"/> para crear un nuevo libro.
        /// </summary>
        /// <param name="command">El comando que contiene los datos para la creación del libro.</param>
        /// <returns>El objeto <see cref="Book"/> creado y persistido.</returns>
        /// <remarks>
        /// Este método:
        /// 1. Crea una nueva instancia de <see cref="Book"/> utilizando los datos proporcionados en el comando.
        /// 2. Añade el nuevo libro al repositorio.
        /// 3. Completa la unidad de trabajo para persistir los cambios en la base de datos.
        /// </remarks>
        public async Task<Book> Handle(CreateBookCommand command)
        {
            var book = new Book(
                command.Title,
                command.Description,
                command.Author,
                command.SalePrice,
                command.Stock,
                command.Cover,
                command.Genre,
                command.Language
            );

            await _bookRepository.AddAsync(book);
            await _unitOfWork.CompleteAsync(); 

            return book; 
        }
        
        /// <summary>
        /// Maneja el comando <see cref="UpdateBookStockCommand"/> para actualizar el stock de un libro.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del libro y la nueva cantidad de stock.</param>
        /// <returns>El objeto <see cref="Book"/> actualizado, o <c>null</c> si el libro no se encuentra.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si el nuevo stock es negativo (aunque el dominio ya lo valida).</exception>
        public async Task<Book?> Handle(UpdateBookStockCommand command)
        {
            var book = await _bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                return null; // Book not found
            }

            // Call the behavior method on the Book aggregate to update its stock
            // The Book aggregate itself validates if newStock is negative.
            book.SetStock(command.NewStock);

            await _unitOfWork.CompleteAsync(); // Persist changes
            return book;
        }
    }
}