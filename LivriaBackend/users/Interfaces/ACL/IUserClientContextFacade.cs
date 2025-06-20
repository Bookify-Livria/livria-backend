using System.Threading.Tasks;

namespace LivriaBackend.users.Interfaces.ACL
{

    public interface IUserClientContextFacade
    {
      
        Task<int> CreateUserClient(string display, string username, string email, string password, string icon, string phrase, string subscription);

      
        Task<bool> ExistsUserClientById(int id);
    }
}