using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.users.Domain.Model.Commands
{
    public record AddFavoriteBookCommand(
        [Required] int UserClientId,
        [Required] int BookId
    );
}