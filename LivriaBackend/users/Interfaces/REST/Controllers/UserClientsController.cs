using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Interfaces.REST.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System; // Para ApplicationException, si tu servicio lanza esa excepción

namespace LivriaBackend.users.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/userclients")] // Define la ruta base para este controlador
    public class UserClientsController : ControllerBase
    {
        private readonly IUserClientCommandService _userClientCommandService;
        private readonly IUserClientQueryService _userClientQueryService;
        private readonly IMapper _mapper;

        public UserClientsController(IUserClientCommandService userClientCommandService, IUserClientQueryService userClientQueryService, IMapper mapper)
        {
            _userClientCommandService = userClientCommandService;
            _userClientQueryService = userClientQueryService;
            _mapper = mapper;
        }

        // GET: api/v1/userclients
        /// <summary>
        /// Obtiene todos los clientes de usuario.
        /// </summary>
        /// <returns>Una lista de recursos de cliente de usuario.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserClientResource>>> GetAllUserClients()
        {
            var query = new GetAllUserClientQuery();
            var userClients = await _userClientQueryService.Handle(query);
            var userClientResources = _mapper.Map<IEnumerable<UserClient>, IEnumerable<UserClientResource>>(userClients);
            return Ok(userClientResources);
        }

        // GET: api/v1/userclients/{id}
        /// <summary>
        /// Obtiene un cliente de usuario por su ID.
        /// </summary>
        /// <param name="id">ID del cliente de usuario.</param>
        /// <returns>El recurso del cliente de usuario si se encuentra; de lo contrario, 404 Not Found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserClientResource>> GetUserClientById(int id)
        {
            var query = new GetUserClientByIdQuery(id);
            var userClient = await _userClientQueryService.Handle(query);

            if (userClient == null)
            {
                return NotFound();
            }

            var userClientResource = _mapper.Map<UserClient, UserClientResource>(userClient);
            return Ok(userClientResource);
        }

        // POST: api/v1/userclients
        /// <summary>
        /// Crea un nuevo cliente de usuario.
        /// </summary>
        /// <param name="resource">Recurso con los datos del nuevo cliente de usuario.</param>
        /// <returns>El recurso del cliente de usuario creado con código 201 Created.</returns>
        [HttpPost]
        public async Task<ActionResult<UserClientResource>> CreateUserClient([FromBody] CreateUserClientResource resource)
        {
            var command = _mapper.Map<CreateUserClientResource, CreateUserClientCommand>(resource);
            var userClient = await _userClientCommandService.Handle(command);

            if (userClient == null) // Esto podría indicar un error de negocio o validación en el servicio
            {
                 return BadRequest("Could not create user client. Check provided data.");
            }

            var userClientResource = _mapper.Map<UserClient, UserClientResource>(userClient);

            // Retorna 201 CreatedAtAction para indicar que el recurso fue creado
            return CreatedAtAction(nameof(GetUserClientById), new { id = userClientResource.Id }, userClientResource);
        }

        // PUT: api/v1/userclients/{id}
        /// <summary>
        /// Actualiza un cliente de usuario existente.
        /// </summary>
        /// <param name="id">ID del cliente de usuario a actualizar.</param>
        /// <param name="resource">Recurso con los datos actualizados del cliente de usuario.</param>
        /// <returns>El recurso del cliente de usuario actualizado; de lo contrario, 404 Not Found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserClientResource>> UpdateUserClient(int id, [FromBody] UpdateUserClientResource resource)
        {
            var command = _mapper.Map<UpdateUserClientResource, UpdateUserClientCommand>(resource);
            command.UserClientId = id; // Asigna el ID de la ruta al comando.

            try
            {
                var userClient = await _userClientCommandService.Handle(command);
                var userClientResource = _mapper.Map<UserClient, UserClientResource>(userClient);
                return Ok(userClientResource); // Retorna 200 OK
            }
            catch (ApplicationException ex) // Captura la excepción si el UserClient no fue encontrado por el servicio
            {
                if (ex.Message.Contains("not found")) // Si el servicio lanza una excepción con este mensaje
                {
                    return NotFound(ex.Message); // Retorna 404 Not Found
                }
                throw; // Relanza otras excepciones inesperadas
            }
        }

        // DELETE: api/v1/userclients/{id}
        /// <summary>
        /// Elimina un cliente de usuario por su ID.
        /// </summary>
        /// <param name="id">ID del cliente de usuario a eliminar.</param>
        /// <returns>204 No Content si la eliminación es exitosa; 404 Not Found si no se encuentra.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserClient(int id)
        {
            var command = new DeleteUserClientCommand(id);
            try
            {
                await _userClientCommandService.Handle(command);
                return NoContent(); // Retorna 204 No Content para eliminación exitosa
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