using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.commerce.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace LivriaBackend.commerce.Interfaces.REST.Controllers
{
    /// <summary>
    /// Controlador RESTful para gestionar las operaciones relacionadas con los libros.
    /// </summary>
    [ApiController]
    [Route("api/v1/books")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)] 

    public class BooksController : ControllerBase
    {
        private readonly IBookQueryService _bookQueryService;
        private readonly IBookCommandService _bookCommandService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BooksController"/>.
        /// </summary>
        /// <param name="bookQueryService">El servicio de consulta de libros.</param>
        /// <param name="bookCommandService">El servicio de comandos de libros.</param>
        /// <param name="mapper">La instancia de AutoMapper para la transformación de objetos.</param>
        public BooksController(IBookQueryService bookQueryService, IBookCommandService bookCommandService, IMapper mapper)
        {
            _bookQueryService = bookQueryService;
            _bookCommandService = bookCommandService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene los datos de todos los libros disponibles en el sistema.
        /// </summary>
        /// <returns>
        /// Una acción de resultado HTTP que contiene una colección de <see cref="BookResource"/>
        /// si la operación es exitosa (código 200 OK).
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary= "Obtener los datos de todos los libros.",
            Description= "Te muestra los datos de los libros."
            
        )]
        public async Task<ActionResult<IEnumerable<BookResource>>> GetAllBooks()
        {
            var query = new GetAllBooksQuery();
            var books = await _bookQueryService.Handle(query);
            var bookResources = _mapper.Map<IEnumerable<BookResource>>(books);
            return Ok(bookResources);
        }
        
        /// <summary>
        /// Obtiene los datos de un libro específico por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del libro.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene un <see cref="BookResource"/> si el libro es encontrado (código 200 OK),
        /// o un resultado NotFound (código 404) si el libro no existe.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de un libro en específico.",
            Description= "Te muestra los datos del libro que buscaste."
        )]
        public async Task<ActionResult<BookResource>> GetBookById(int id)
        {
            var query = new GetBookByIdQuery(id);
            var book = await _bookQueryService.Handle(query);

            if (book == null)
            {
                return NotFound();
            }

            var bookResource = _mapper.Map<BookResource>(book);
            return Ok(bookResource);
        }

        /// <summary>
        /// Crea un nuevo libro en el sistema.
        /// </summary>
        /// <param name="resource">Los datos del nuevo libro a crear.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene el <see cref="BookResource"/> del libro creado
        /// con un código 201 CreatedAtAction si la operación es exitosa.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
            Summary= "Crear un nuevo libro.",
            Description= "Crea un nuevo libro en el sistema."
        )]
        public async Task<ActionResult<BookResource>> CreateBook([FromBody] CreateBookResource resource)
        {
            var createCommand = _mapper.Map<CreateBookCommand>(resource);
            var book = await _bookCommandService.Handle(createCommand); 

            var bookResource = _mapper.Map<BookResource>(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookResource);
        }
        
        /// <summary>
        /// Actualiza la cantidad de stock de un libro existente por su ID.
        /// </summary>
        /// <param name="bookId">El identificador único del libro cuyo stock se actualizará.</param>
        /// <param name="resource">El recurso que contiene la nueva cantidad de stock.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene el <see cref="BookResource"/> actualizado
        /// si la operación fue exitosa (código 200 OK).
        /// Retorna 400 Bad Request si la validación del stock falla o los datos son inválidos.
        /// Retorna 404 Not Found si el libro no existe.
        /// Retorna 500 Internal Server Error si ocurre un error inesperado.
        /// </returns>
        [HttpPut("{bookId}/stock")] // PUT api/v1/books/{bookId}/stock
        [SwaggerOperation(
            Summary = "Actualizar el stock de un libro.",
            Description = "Permite cambiar la cantidad de stock disponible de un libro."
        )]
        [ProducesResponseType(typeof(BookResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBookStock(int bookId, [FromBody] UpdateBookStockResource resource)
        {
            // Valida el DTO de entrada usando Data Annotations
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Crea el comando. El BookId viene de la URL, el NewStock del cuerpo de la solicitud.
            var command = new UpdateBookStockCommand(bookId, resource.NewStock);

            try
            {
                // Delega la lógica de negocio al servicio de comandos.
                var updatedBook = await _bookCommandService.Handle(command);

                // Si el servicio devuelve null, el libro no fue encontrado.
                if (updatedBook == null)
                {
                    return NotFound(new { message = $"Book with ID {bookId} not found." });
                }

                // Mapea la entidad de dominio actualizada a un recurso para la respuesta de la API.
                var bookResource = _mapper.Map<BookResource>(updatedBook);
                return Ok(bookResource); // 200 OK con el recurso actualizado
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Captura específicamente si el stock es negativo (aunque Resource ya valida esto, es una segunda línea de defensa)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada.
                return StatusCode(500, new { message = "An unexpected error occurred while updating book stock: " + ex.Message });
            }
        }

    }
}