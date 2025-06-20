namespace LivriaBackend.users.Domain.Model.Commands
{
    public class UpdateUserAdminCommand
    {
        // Añade un setter para el ID si no lo tiene, o haz la propiedad mutable
        public int UserAdminId { get; set; } // <--- ASEGÚRATE de que esto tenga un 'set;'

        public string? Display { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool AdminAccess { get; set; }
        public string? SecurityPin { get; set; }

        // Constructor para uso de AutoMapper (o para inicialización sin ID al mapear)
        public UpdateUserAdminCommand() { }

        // Constructor para uso en controladores donde el ID viene de la ruta
        public UpdateUserAdminCommand(int userAdminId, string? display, string? username, string? email, string? password, bool adminAccess, string? securityPin)
        {
            UserAdminId = userAdminId;
            Display = display;
            Username = username;
            Email = email;
            Password = password;
            AdminAccess = adminAccess;
            SecurityPin = securityPin;
        }
    }
}