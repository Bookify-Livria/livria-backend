namespace LivriaBackend.users.Domain.Model.Commands
{
    public class UpdateUserClientCommand
    {
        // Añade un setter para el ID si no lo tiene, o haz la propiedad mutable
        public int UserClientId { get; set; } // <--- ASEGÚRATE de que esto tenga un 'set;'

        public string? Display { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Icon { get; set; }
        public string? Phrase { get; set; }
        public string? Subscription { get; set; } // Asumiendo que UserClient tiene suscripción

        // Constructor para uso de AutoMapper (o para inicialización sin ID al mapear)
        public UpdateUserClientCommand() { }

        // Constructor para uso en controladores donde el ID viene de la ruta
        public UpdateUserClientCommand(int userClientId, string? display, string? username, string? email, string? password, string? icon, string? phrase, string? subscription)
        {
            UserClientId = userClientId;
            Display = display;
            Username = username;
            Email = email;
            Password = password;
            Icon = icon;
            Phrase = phrase;
            Subscription = subscription;
        }
    }
}