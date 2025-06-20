using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.users.Infrastructure.Repositories
{
    // ESTO ES UNA IMPLEMENTACIÓN MOCK/IN-MEMORY PARA DEMOSTRACIÓN.
    // En un proyecto real, esto interactuaría con una base de datos (EF Core, Dapper, etc.).
    public class UserAdminRepository : IUserAdminRepository
    {
        private static readonly List<UserAdmin> _userAdmins = new List<UserAdmin>();
        private static int _nextId = 1; // Para simular IDs de DB

        public UserAdminRepository()
        {
            // Inicializar algunos datos de ejemplo si la lista está vacía
            if (!_userAdmins.Any())
            {
                _userAdmins.Add(new UserAdmin(_nextId++, "Admin Display 1", "admin1", "admin1@example.com", "password123", true, "1234"));
                _userAdmins.Add(new UserAdmin(_nextId++, "Admin Display 2", "admin2", "admin2@example.com", "password456", true, "5678"));
            }
        }

        public Task<UserAdmin> GetByIdAsync(int id)
        {
            return Task.FromResult(_userAdmins.FirstOrDefault(ua => ua.Id == id));
        }

        public Task<IEnumerable<UserAdmin>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<UserAdmin>>(_userAdmins);
        }

        public Task AddAsync(UserAdmin userAdmin)
        {
            userAdmin.GetType().GetProperty("Id").SetValue(userAdmin, _nextId++); // Simula asignación de ID por DB
            _userAdmins.Add(userAdmin);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(UserAdmin userAdmin)
        {
            var existingUserAdmin = _userAdmins.FirstOrDefault(ua => ua.Id == userAdmin.Id);
            if (existingUserAdmin != null)
            {
                // Actualiza las propiedades manualmente (o usa un mapeador como AutoMapper)
                existingUserAdmin.Display = userAdmin.Display;
                existingUserAdmin.Username = userAdmin.Username;
                existingUserAdmin.Email = userAdmin.Email;
                existingUserAdmin.Password = userAdmin.Password;
                existingUserAdmin.AdminAccess = userAdmin.AdminAccess;
                existingUserAdmin.SecurityPin = userAdmin.SecurityPin;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _userAdmins.RemoveAll(ua => ua.Id == id);
            return Task.CompletedTask;
        }
    }
}