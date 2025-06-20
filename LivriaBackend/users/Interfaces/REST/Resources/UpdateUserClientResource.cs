using System.ComponentModel.DataAnnotations; // Para validaciones

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    // Cambiado a 'public record class'
    public record class UpdateUserClientResource : UpdateUserResource
    {
        // Propiedades específicas de UserClient para la actualización
        // Hacemos nullable con 'init' si son opcionales en la actualización
        public string? Icon { get; init; }
        public string? Phrase { get; init; }
        public string? Subscription { get; init; } // Asumiendo que UserClient tiene suscripción

        // Constructor explícito para inicializar todas las propiedades
        public UpdateUserClientResource(
            string? display,
            string? username,
            string? email,
            string? password,
            string? icon,
            string? phrase,
            string? subscription
        ) : base(display, username, email, password, icon, phrase)
        {
            Icon = icon;
            Phrase = phrase;
            Subscription = subscription;
        }
        
        // Constructor sin parámetros para la deserialización y mapeo si es necesario
        public UpdateUserClientResource() : base() { }
    }
}