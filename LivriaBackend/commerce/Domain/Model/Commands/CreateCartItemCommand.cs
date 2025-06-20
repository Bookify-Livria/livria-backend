using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Domain.Model.Commands
{
    public record CreateCartItemCommand(
        [Required] int BookId,
        [Required] [Range(1, int.MaxValue)] int Quantity,
        [Required] int UserClientId
    );
}