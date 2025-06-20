namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public class UpdateUserResource // No hereda de ningún otro recurso directamente
    {
        // Propiedades comunes de un usuario que se pueden actualizar
        public string? Display { get; set; } // Hacemos estas propiedades nullable si no todas son obligatorias en la actualización
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } // Considera siempre si quieres actualizar la contraseña directamente en un PUT genérico
        public string? Icon { get; set; }
        public string? Phrase { get; set; }
    }
}