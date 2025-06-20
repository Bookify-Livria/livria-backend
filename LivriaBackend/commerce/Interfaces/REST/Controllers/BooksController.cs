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

namespace LivriaBackend.commerce.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    [Produces(MediaTypeNames.Application.Json)]
    public class BooksController : ControllerBase
    {
        private readonly IBookQueryService _bookQueryService;
        private readonly IBookCommandService _bookCommandService;
        private readonly IMapper _mapper;

        public BooksController(IBookQueryService bookQueryService, IBookCommandService bookCommandService, IMapper mapper)
        {
            _bookQueryService = bookQueryService;
            _bookCommandService = bookCommandService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResource>>> GetAllBooks()
        {
            var query = new GetAllBooksQuery();
            var books = await _bookQueryService.Handle(query);
            var bookResources = _mapper.Map<IEnumerable<BookResource>>(books);
            return Ok(bookResources);
        }
        
        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<BookResource>> CreateBook([FromBody] CreateBookResource resource)
        {
            var createCommand = _mapper.Map<CreateBookCommand>(resource);
            var book = await _bookCommandService.Handle(createCommand); 

            var bookResource = _mapper.Map<BookResource>(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookResource);
        }

    }
}