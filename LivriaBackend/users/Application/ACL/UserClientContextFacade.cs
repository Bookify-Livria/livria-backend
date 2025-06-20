using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Queries; // Necesario para GetUserClientByIdQuery
using LivriaBackend.users.Domain.Model.Services; // Necesario para IUserClientCommandService, IUserClientQueryService
using LivriaBackend.users.Interfaces.ACL; // Necesario para IUserClientContextFacade
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.ACL
{
    /// <summary>
    /// Facade for accessing UserClient related operations from other bounded contexts.
    /// </summary>
    public class UserClientContextFacade : IUserClientContextFacade
    {
        private readonly IUserClientCommandService _userClientCommandService;
        private readonly IUserClientQueryService _userClientQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClientContextFacade"/> class.
        /// </summary>
        /// <param name="userClientCommandService">The command service for UserClient operations.</param>
        /// <param name="userClientQueryService">The query service for UserClient operations.</param>
        public UserClientContextFacade(
            IUserClientCommandService userClientCommandService,
            IUserClientQueryService userClientQueryService
        )
        {
            _userClientCommandService = userClientCommandService;
            _userClientQueryService = userClientQueryService;
        }

        // inheritedDoc
        public async Task<int> CreateUserClient(string display, string username, string email, string password, string icon, string phrase, string subscription)
        {
            var createCommand = new CreateUserClientCommand(display, username, email, password, icon, phrase, subscription);
            var userClient = await _userClientCommandService.Handle(createCommand);
            return userClient?.Id ?? 0;
        }

        // inheritedDoc
        public async Task<bool> ExistsUserClientById(int id)
        {
            // Este método usa el QueryService interno para verificar la existencia por ID.
            var query = new GetUserClientByIdQuery(id);
            var userClient = await _userClientQueryService.Handle(query);
            return userClient != null; // Devuelve true si se encontró un UserClient con ese ID
        }
    }
}