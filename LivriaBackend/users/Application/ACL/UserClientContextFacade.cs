using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Queries; 
using LivriaBackend.users.Domain.Model.Services; 
using LivriaBackend.users.Interfaces.ACL; 
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.ACL
{

    public class UserClientContextFacade : IUserClientContextFacade
    {
        private readonly IUserClientCommandService _userClientCommandService;
        private readonly IUserClientQueryService _userClientQueryService;

      
        public UserClientContextFacade(
            IUserClientCommandService userClientCommandService,
            IUserClientQueryService userClientQueryService
        )
        {
            _userClientCommandService = userClientCommandService;
            _userClientQueryService = userClientQueryService;
        }

        
        public async Task<int> CreateUserClient(string display, string username, string email, string password, string icon, string phrase, string subscription)
        {
            var createCommand = new CreateUserClientCommand(display, username, email, password, icon, phrase, subscription);
            var userClient = await _userClientCommandService.Handle(createCommand);
            return userClient?.Id ?? 0;
        }

        
        public async Task<bool> ExistsUserClientById(int id)
        {
            
            var query = new GetUserClientByIdQuery(id);
            var userClient = await _userClientQueryService.Handle(query);
            return userClient != null; 
        }
    }
}