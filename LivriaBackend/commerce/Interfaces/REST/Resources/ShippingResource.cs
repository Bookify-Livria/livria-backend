using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record ShippingResource(
        [Required] string Address,
        [Required] string City,
        [Required] string District,
        string Reference
    );
}