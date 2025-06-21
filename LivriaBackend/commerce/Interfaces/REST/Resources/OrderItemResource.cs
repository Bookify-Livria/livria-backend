namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record OrderItemResource(
        int Id,
        int BookId, 
        string BookTitle,
        string BookAuthor,
        decimal BookPrice,
        string BookCover,
        int Quantity,
        decimal ItemTotal
    );
}