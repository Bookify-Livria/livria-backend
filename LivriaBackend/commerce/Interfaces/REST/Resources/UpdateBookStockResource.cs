using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource para actualizar el stock de un libro.
    /// </summary>
    public class UpdateBookStockResource
    {
        /// <summary>
        /// La nueva cantidad de stock para el libro. No puede ser negativa.
        /// </summary>
        [Required(ErrorMessage = "El nuevo stock es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int NewStock { get; set; }
    }
}