using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Domain.Model.Commands
{
    public record RemoveCartItemCommand(
        [Required] int CartItemId,
        [Required] int UserClientId
    );
}