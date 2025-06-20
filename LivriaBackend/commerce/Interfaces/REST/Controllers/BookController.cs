using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.Books.Domain.Model.Services;
using LivriaBackend.commerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/books")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> GetAll() => await _bookService.ListAsync();

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Book book)
    {
        var result = await _bookService.CreateAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Book book)
    {
        var updated = await _bookService.UpdateAsync(id, book);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);
        if (deleted == null) return NotFound();
        return NoContent();
    }
}