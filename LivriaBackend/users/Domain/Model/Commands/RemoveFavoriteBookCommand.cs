using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.users.Domain.Model.Commands
{
    public record RemoveFavoriteBookCommand(
        [Required] int UserClientId,
        [Required] int BookId
    );
}