namespace LivriaBackend.users.Interfaces.REST.Resources
{
    // Cambiado a 'public record class' para permitir herencia y propiedades 'init'
    public record class UpdateUserResource
    {
        // Propiedades comunes de un usuario que se pueden actualizar.
        // Usar 'init' para inmutabilidad después de la creación/deserialización,
        // pero permitiendo la inicialización de propiedades individuales.
        public string? Display { get; init; }
        public string? Username { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; } // Considera siempre si quieres actualizar la contraseña directamente en un PUT genérico
        public string? Icon { get; init; } // Estas pueden ser relevantes para UserClient
        public string? Phrase { get; init; } // Estas pueden ser relevantes para UserClient

        // Constructor con todos los parámetros para uso directo o mapeo si se necesita.
        public UpdateUserResource(string? display, string? username, string? email, string? password, string? icon, string? phrase)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
            Icon = icon;
            Phrase = phrase;
        }

        // Constructor sin parámetros para la deserialización y mapeo (ej. AutoMapper)
        public UpdateUserResource() { }
    }
}