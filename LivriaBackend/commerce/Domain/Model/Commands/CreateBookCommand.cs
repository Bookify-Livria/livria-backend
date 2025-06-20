namespace LivriaBackend.commerce.Domain.Model.Commands
{
    public record CreateBookCommand(
        string Title,
        string Description,
        string Author,
        decimal Price,
        int Stock,
        string Cover,
        string Genre,
        string Language
    );
}