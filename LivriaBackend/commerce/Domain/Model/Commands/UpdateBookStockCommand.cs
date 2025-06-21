using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Domain.Model.Commands
{
    /// <summary>
    /// Representa un comando para actualizar el stock de un libro existente.
    /// </summary>
    /// <param name="BookId">El identificador único del libro cuyo stock se actualizará.</param>
    /// <param name="NewStock">La nueva cantidad de stock para el libro.</param>
    public record UpdateBookStockCommand(
        [Required] int BookId,
        [Required] int NewStock
    );
}