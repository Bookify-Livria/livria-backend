using System;
using System.Collections.Generic;

namespace LivriaBackend.users.Domain.Model.Aggregates
{
    public class UserAdmin : User
    {
        public bool AdminAccess { get; private set; }
        public string SecurityPin { get; private set; }

        protected UserAdmin() : base() { }

        public UserAdmin(string display, string username, string email, string password, bool adminAccess, string securityPin)
            : base(display, username, email, password)
        {
            AdminAccess = adminAccess;
            SecurityPin = securityPin;
        }

        public void Update(string display, string username, string email, string password, bool adminAccess, string securityPin)
        {
            base.UpdateUserProperties(display, username, email, password);
            AdminAccess = adminAccess;
            SecurityPin = securityPin;
        }
    }
}