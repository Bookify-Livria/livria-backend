using System.Threading.Tasks;

namespace LivriaBackend.users.Interfaces.ACL
{
    /// <summary>
    /// Facade for accessing UserClient related operations from other bounded contexts.
    /// </summary>
    public interface IUserClientContextFacade
    {
        /// <summary>
        /// Creates a new UserClient in the User Bounded Context.
        /// </summary>
        /// <param name="display">The display name of the user client.</param>
        /// <param name="username">The username of the user client.</param>
        /// <param name="email">The email of the user client.</param>
        /// <param name="password">The password of the user client.</param>
        /// <param name="icon">The icon identifier for the user client.</param>
        /// <param name="phrase">A custom phrase for the user client.</param>
        /// <param name="subscription">The subscription type of the user client.</param>
        /// <returns>The ID of the created UserClient if successful, 0 otherwise.</returns>
        Task<int> CreateUserClient(string display, string username, string email, string password, string icon, string phrase, string subscription);

        /// <summary>
        /// Checks if a UserClient exists by their ID.
        /// </summary>
        /// <param name="id">The ID of the user client to check.</param>
        /// <returns>True if the UserClient exists, false otherwise.</returns>
        Task<bool> ExistsUserClientById(int id);
    }
}