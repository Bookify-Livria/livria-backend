using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateCartItemResource(
        [Required] int BookId,
        [Required] [Range(1, int.MaxValue)] int Quantity,
        [Required] int UserClientId
    );
}