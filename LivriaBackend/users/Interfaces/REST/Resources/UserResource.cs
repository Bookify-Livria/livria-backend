namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Display { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        // La contraseña NUNCA debe exponerse en un recurso de respuesta
    }
}