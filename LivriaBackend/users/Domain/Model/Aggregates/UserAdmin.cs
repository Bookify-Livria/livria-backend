using System;
using System.Collections.Generic;

namespace LivriaBackend.users.Domain.Model.Aggregates
{
    /// <summary>
    /// Representa un usuario con privilegios administrativos en el sistema.
    /// Hereda de la clase base <see cref="User"/> y añade propiedades específicas de administrador.
    /// </summary>
    public class UserAdmin : User
    {
        /// <summary>
        /// Obtiene un valor que indica si el administrador tiene acceso a las funcionalidades administrativas.
        /// </summary>
        public bool AdminAccess { get; private set; }

        /// <summary>
        /// Obtiene el pin de seguridad asociado a la cuenta del administrador.
        /// </summary>
        public string SecurityPin { get; private set; }

        /// <summary>
        /// Constructor protegido sin parámetros, típicamente utilizado por ORMs como Entity Framework Core.
        /// </summary>
        protected UserAdmin() : base() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserAdmin"/> con las propiedades especificadas.
        /// </summary>
        /// <param name="display">El nombre visible o alias del administrador.</param>
        /// <param name="username">El nombre de usuario único del administrador.</param>
        /// <param name="email">La dirección de correo electrónico del administrador.</param>
        /// <param name="password">La contraseña del administrador (asumida como segura).</param>
        /// <param name="adminAccess">Indica si el administrador tiene acceso de administrador.</param>
        /// <param name="securityPin">El pin de seguridad del administrador.</param>
        public UserAdmin(string display, string username, string email, string password, bool adminAccess, string securityPin)
            : base(display, username, email, password)
        {
            AdminAccess = adminAccess;
            SecurityPin = securityPin;
        }

        /// <summary>
        /// Actualiza las propiedades de un administrador de usuario.
        /// Utiliza el método base <see cref="User.UpdateUserProperties"/> para actualizar las propiedades de usuario comunes.
        /// </summary>
        /// <param name="display">El nuevo nombre visible o alias.</param>
        /// <param name="username">El nuevo nombre de usuario.</param>
        /// <param name="email">La nueva dirección de correo electrónico.</param>
        /// <param name="password">La nueva contraseña.</param>
        /// <param name="adminAccess">El nuevo estado de acceso de administrador.</param>
        /// <param name="securityPin">El nuevo pin de seguridad.</param>
        public void Update(string display, string username, string email, string password, bool adminAccess, string securityPin)
        {
            base.UpdateUserProperties(display, username, email, password);
            AdminAccess = adminAccess;
            SecurityPin = securityPin;
        }
    }
}