using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Domain.Model.Commands
{
    public record UpdateCartItemQuantityCommand(
        [Required] int CartItemId,
        [Required] [Range(0, int.MaxValue)] int NewQuantity,
        [Required] int UserClientId
    );
}