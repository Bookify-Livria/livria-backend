using LivriaBackend.commerce.Domain.Model.Aggregates; 
using LivriaBackend.users.Domain.Model.Aggregates; 
using System; 

namespace LivriaBackend.commerce.Domain.Model.Entities
{
    /// <summary>
    /// Representa un ítem dentro de un carrito de compras. Es una entidad de dominio.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Obtiene el identificador único del ítem del carrito.
        /// </summary>
        public int Id { get; private set; } 

        /// <summary>
        /// Obtiene el identificador único del libro asociado a este ítem del carrito.
        /// </summary>
        public int BookId { get; private set; }

        /// <summary>
        /// Obtiene el objeto <see cref="Book"/> asociado a este ítem del carrito.
        /// </summary>
        public Book Book { get; private set; }

        /// <summary>
        /// Obtiene o establece la cantidad del libro en este ítem del carrito.
        /// Este valor es mutable a través de métodos de comportamiento.
        /// </summary>
        public int Quantity { get; private set; } 

        /// <summary>
        /// Obtiene el identificador único del cliente de usuario al que pertenece este ítem del carrito.
        /// </summary>
        public int UserClientId { get; private set; } 

        /// <summary>
        /// Obtiene el objeto <see cref="UserClient"/> al que pertenece este ítem del carrito.
        /// </summary>
        public UserClient UserClient { get; private set; } 

        /// <summary>
        /// Constructor protegido para uso de frameworks ORM (como Entity Framework Core).
        /// No debe ser utilizado directamente para la creación de instancias de <see cref="CartItem"/>.
        /// </summary>
        protected CartItem() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CartItem"/>.
        /// </summary>
        /// <param name="bookId">El identificador del libro.</param>
        /// <param name="quantity">La cantidad del libro.</param>
        /// <param name="userClientId">El identificador del cliente de usuario.</param>
        public CartItem(int bookId, int quantity, int userClientId)
        {
            BookId = bookId;
            Quantity = quantity;
            UserClientId = userClientId;
        }

        /// <summary>
        /// Actualiza la cantidad de este ítem del carrito a un nuevo valor.
        /// </summary>
        /// <param name="newQuantity">La nueva cantidad deseada.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si la nueva cantidad es negativa.</exception>
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity cannot be negative.");
            }
            Quantity = newQuantity;
        }

        /// <summary>
        /// Incrementa la cantidad de este ítem del carrito por un valor especificado.
        /// </summary>
        /// <param name="amount">La cantidad a incrementar (por defecto es 1).</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si la cantidad a incrementar es negativa.</exception>
        public void IncrementQuantity(int amount = 1)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to increment cannot be negative.");
            }
            Quantity += amount;
        }

        /// <summary>
        /// Decrementa la cantidad de este ítem del carrito por un valor especificado.
        /// La cantidad nunca será menor que cero.
        /// </summary>
        /// <param name="amount">La cantidad a decrementar (por defecto es 1).</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si la cantidad a decrementar es negativa.</exception>
        public void DecrementQuantity(int amount = 1)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to decrement cannot be negative.");
            }
            Quantity -= amount;
            if (Quantity < 0) Quantity = 0;
        }
    }
}