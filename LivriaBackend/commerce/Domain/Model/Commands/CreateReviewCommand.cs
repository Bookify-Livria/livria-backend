namespace LivriaBackend.commerce.Domain.Model.Commands
{
    public record CreateReviewCommand(
        int BookId,
        int UserClientId,
        string Content
    );
}