using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Interfaces.REST.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System; // Para ApplicationException

namespace LivriaBackend.users.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/useradmins")] // Define la ruta base para este controlador
    public class UserAdminsController : ControllerBase
    {
        private readonly IUserAdminCommandService _userAdminCommandService;
        private readonly IUserAdminQueryService _userAdminQueryService;
        private readonly IMapper _mapper;

        public UserAdminsController(IUserAdminCommandService userAdminCommandService, IUserAdminQueryService userAdminQueryService, IMapper mapper)
        {
            _userAdminCommandService = userAdminCommandService;
            _userAdminQueryService = userAdminQueryService;
            _mapper = mapper;
        }

        // GET: api/v1/useradmins
        /// <summary>
        /// Obtiene todos los administradores de usuario.
        /// </summary>
        /// <returns>Una lista de recursos de administrador de usuario.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdminResource>>> GetAllUserAdmins()
        {
            var query = new GetAllUserAdminQuery();
            var userAdmins = await _userAdminQueryService.Handle(query);
            var userAdminResources = _mapper.Map<IEnumerable<UserAdmin>, IEnumerable<UserAdminResource>>(userAdmins);
            return Ok(userAdminResources);
        }

        // PUT: api/v1/useradmins/{id}
        /// <summary>
        /// Actualiza un administrador de usuario existente.
        /// </summary>
        /// <param name="id">ID del administrador de usuario a actualizar.</param>
        /// <param name="resource">Recurso con los datos actualizados del administrador de usuario.</param>
        /// <returns>El recurso del administrador de usuario actualizado; de lo contrario, 404 Not Found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserAdminResource>> UpdateUserAdmin(int id, [FromBody] UpdateUserAdminResource resource)
        {
            var command = _mapper.Map<UpdateUserAdminResource, UpdateUserAdminCommand>(resource);
            command.UserAdminId = id; // Asigna el ID de la ruta al comando.

            try
            {
                var userAdmin = await _userAdminCommandService.Handle(command);
                var userAdminResource = _mapper.Map<UserAdmin, UserAdminResource>(userAdmin);
                return Ok(userAdminResource);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(ex.Message);
                }
                throw;
            }
        }
    }
}