using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record UpdateCartItemQuantityResource(
        [Required] [Range(0, int.MaxValue)] int NewQuantity 

    );
}