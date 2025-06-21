using System;
using System.Collections.Generic;
using LivriaBackend.commerce.Domain.Model.Entities;

namespace LivriaBackend.commerce.Domain.Model.Aggregates
{
    /// <summary>
    /// Representa la entidad agregada 'Libro' en el dominio de comercio.
    /// Un <see cref="Book"/> es un objeto con una identidad global y es la raíz de un agregado,
    /// lo que significa que controla la vida útil de las entidades contenidas como <see cref="Review"/>.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Obtiene el identificador único del libro.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Obtiene el título del libro.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Obtiene la descripción detallada del libro.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Obtiene el autor del libro.
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Obtiene el precio del libro.
        /// </summary>
        public decimal SalePrice { get; private set; }

        
        
        /// <summary>
        /// Obtiene o establece la cantidad de stock disponible del libro.
        /// Este valor es mutable a través de métodos de comportamiento.
        /// </summary>
        public int Stock { get; private set; }

        /// <summary>
        /// Obtiene la URL o ruta de la imagen de la portada del libro.
        /// </summary>
        public string Cover { get; private set; }

        /// <summary>
        /// Obtiene el género al que pertenece el libro.
        /// </summary>
        public string Genre { get; private set; }

        /// <summary>
        /// Obtiene el idioma en el que está escrito el libro.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Obtiene la colección de reseñas asociadas a este libro.
        /// Esta colección es privada y solo se puede modificar a través de métodos de comportamiento del agregado.
        /// </summary>
        public ICollection<Review> Reviews { get; private set; } = new List<Review>();

        
        private static readonly HashSet<string> AllowedGenres = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "literatura", "noficcion", "mangasycomics", "juvenil", "infantil", "ebooks"
        };
        
        /// <summary>
        /// Constructor protegido para uso de frameworks ORM (como Entity Framework Core).
        /// No debe ser utilizado directamente para la creación de instancias de <see cref="Book"/>.
        /// </summary>
        protected Book() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Book"/> con los detalles proporcionados.
        /// </summary>
        /// <param name="title">El título del libro.</param>
        /// <param name="description">La descripción del libro.</param>
        /// <param name="author">El autor del libro.</param>
        /// <param name="salePrice">El precio del libro.</param>
        /// <param name="stock">La cantidad inicial de stock disponible.</param>
        /// <param name="cover">La URL o ruta de la imagen de la portada.</param>
        /// <param name="genre">El género del libro.</param>
        /// <param name="language">El idioma del libro.</param>
        /// <exception cref="ArgumentException">Se lanza si el idioma no es 'english' o 'español'.</exception>
        public Book(string title, string description, string author, decimal salePrice, int stock, string cover, string genre, string language)
        {
            if (string.IsNullOrEmpty(language) || !(language.Equals("english", StringComparison.OrdinalIgnoreCase) || language.Equals("español", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("El idioma del libro debe ser 'english' o 'español'.", nameof(language));
            }
            
            if (string.IsNullOrEmpty(genre) || !AllowedGenres.Contains(genre))
            {
                throw new ArgumentException($"El género del libro debe ser uno de los siguientes: {string.Join(", ", AllowedGenres)}.", nameof(genre));
            }

            Title = title;
            Description = description;
            Author = author;
            SalePrice = salePrice;
            Stock = stock; 
            Cover = cover;
            Genre = genre;
            Language = language;
        }
        
        /// <summary>
        /// Disminuye la cantidad de stock del libro por la cantidad especificada.
        /// </summary>
        /// <param name="quantity">La cantidad por la cual disminuir el stock.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si la cantidad es negativa.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si la cantidad a disminuir es mayor que el stock actual.</exception>
        public void DecreaseStock(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity to decrease cannot be negative.");
            }
            if (Stock < quantity)
            {
                throw new InvalidOperationException($"Insufficient stock. Available: {Stock}, Requested: {quantity}.");
            }
            Stock -= quantity;
        }

        /// <summary>
        /// Establece la cantidad de stock del libro a un nuevo valor.
        /// </summary>
        /// <param name="newStock">La nueva cantidad de stock. No puede ser negativa.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si el nuevo stock es negativo.</exception>
        public void SetStock(int newStock)
        {
            if (newStock < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newStock), "El stock no puede ser negativo.");
            }
            Stock = newStock;
        }
        
        /// <summary>
        /// Actualiza todos los detalles mutables del libro.
        /// </summary>
        /// <param name="title">El nuevo título del libro.</param>
        /// <param name="description">La nueva descripción del libro.</param>
        /// <param name="author">El nuevo autor del libro.</param>
        /// <param name="salePrice">El nuevo precio del libro.</param>
        /// <param name="stock">La nueva cantidad de stock disponible.</param>
        /// <param name="cover">La nueva URL o ruta de la imagen de la portada.</param>
        /// <param name="genre">El nuevo género del libro.</param>
        /// <param name="language">El nuevo idioma del libro.</param>
        /// <exception cref="ArgumentException">Se lanza si el idioma no es 'english' o 'español'.</exception>
        public void Update(string title, string description, string author, decimal salePrice, int stock, string cover, string genre, string language)
        {
            if (string.IsNullOrEmpty(language) || !(language.Equals("english", StringComparison.OrdinalIgnoreCase) || language.Equals("español", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("El idioma del libro debe ser 'english' o 'español'.", nameof(language));
            }
            
            if (string.IsNullOrEmpty(genre) || !AllowedGenres.Contains(genre))
            {
                throw new ArgumentException($"El género del libro debe ser uno de los siguientes: {string.Join(", ", AllowedGenres)}.", nameof(genre));
            }
            

            Title = title;
            Description = description;
            Author = author;
            SalePrice = salePrice;
            Stock = stock;
            Cover = cover;
            Genre = genre;
            Language = language;
        }

        /// <summary>
        /// Añade una nueva reseña a la colección de reseñas del libro.
        /// </summary>
        /// <param name="review">El objeto <see cref="Review"/> a añadir. Debe tener el mismo BookId que este libro.</param>
        public void AddReview(Review review)
        {
            if (review != null && review.BookId == this.Id)
            {
                Reviews.Add(review);
            }
        }
    }
}