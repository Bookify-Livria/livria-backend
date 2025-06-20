using System.Collections.Generic;


namespace LivriaBackend.users.Interfaces.REST.Resources
{

    public class UserAdminResource : UserResource // Hereda de UserResource
    {
        public bool AdminAccess { get; set; }
        public string SecurityPin { get; set; } // Considera si realmente quieres exponer el SecurityPin
        // Si es un PIN sensible, es mejor no incluirlo aquí o encriptarlo.
    }
}