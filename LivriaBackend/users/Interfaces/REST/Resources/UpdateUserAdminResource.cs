namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public class UpdateUserAdminResource : UpdateUserResource // Hereda de UpdateUserResource
    {
        // Propiedades específicas de UserAdmin para la actualización
        public bool AdminAccess { get; set; }
        public string SecurityPin { get; set; }
        // Nota: Considera si el SecurityPin realmente debe actualizarse o exponerse de esta manera.
        // Para datos sensibles, a menudo se manejan con un endpoint separado o un proceso seguro.
    }
}