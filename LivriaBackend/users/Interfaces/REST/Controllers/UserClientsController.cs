using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Interfaces.REST.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Linq;
using LivriaBackend.commerce.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace LivriaBackend.users.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/userclients")]
    public class UserClientsController : ControllerBase
    {
        private readonly IUserClientCommandService _userClientCommandService;
        private readonly IUserClientQueryService _userClientQueryService;
        private readonly IMapper _mapper;

        public UserClientsController(
            IUserClientCommandService userClientCommandService,
            IUserClientQueryService userClientQueryService,
            IMapper mapper)
        {
            _userClientCommandService = userClientCommandService;
            _userClientQueryService = userClientQueryService;
            _mapper = mapper;
        }
        
        
        [HttpPost]
        [SwaggerOperation(
            Summary= "Añadir nuevo cliente.",
            Description= "Crea un nuevo cliente en el sistema."
            )]
        public async Task<ActionResult<UserClientResource>> CreateUserClient([FromBody] CreateUserClientResource resource)
        {
            var command = _mapper.Map<CreateUserClientCommand>(resource);
            var userClient = await _userClientCommandService.Handle(command);
            var userClientResource = _mapper.Map<UserClientResource>(userClient);
            return CreatedAtAction(nameof(GetUserClientById), new { id = userClientResource.Id }, userClientResource);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary= "Obtener los datos de todos los clientes.",
            Description= "Te muestra los datos de los clientes."
            
        )]
        public async Task<ActionResult<IEnumerable<UserClientResource>>> GetAllUserClients()
        {
            var query = new GetAllUserClientQuery();
            var userClients = await _userClientQueryService.Handle(query);
            var userClientResources = _mapper.Map<IEnumerable<UserClientResource>>(userClients);
            return Ok(userClientResources);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de un usuario en específico.",
            Description= "Te muestra los datos del usuario que buscaste."
        )]
        public async Task<ActionResult<UserClientResource>> GetUserClientById(int id)
        {
            var query = new GetUserClientByIdQuery(id);
            var userClient = await _userClientQueryService.Handle(query);
            if (userClient == null)
            {
                return NotFound();
            }
            var userClientResource = _mapper.Map<UserClientResource>(userClient);
            return Ok(userClientResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary= "Actualizar los datos de un UserClient existente.",
            Description= "Te permite modificar los datos de un UserClient previamente creado."
        )]
        public async Task<ActionResult<UserClientResource>> UpdateUserClient(int id, [FromBody] UpdateUserClientResource resource)
        {
            var command = new UpdateUserClientCommand(
                id,
                resource.Display,
                resource.Username,
                resource.Email,
                resource.Password,
                resource.Icon,
                resource.Phrase,
                resource.Subscription
            );

            try
            {
                var updatedUserClient = await _userClientCommandService.Handle(command);
                var userClientResource = _mapper.Map<UserClientResource>(updatedUserClient);
                return Ok(userClientResource);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while updating the user client.");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary= "Eliminar un UserClient previamente creado.",
            Description= "Elimina un UserClient del sistema."
        )]
        public async Task<IActionResult> DeleteUserClient(int id)
        {
            var command = new DeleteUserClientCommand(id);
            try
            {
                await _userClientCommandService.Handle(command);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while deleting the user client.");
            }
        }
        
         [HttpPost("{userClientId}/favorites/{bookId}")]
         [SwaggerOperation(
             Summary= "Agregar un libro existente como favorito.",
             Description= "Agrega un libro existente como favorito en el sistema."
         )]
        public async Task<ActionResult<UserClientResource>> AddBookToFavorites(int userClientId, int bookId)
        {
            var command = new AddFavoriteBookCommand(userClientId, bookId);
            try
            {
                var userClient = await _userClientCommandService.Handle(command);
                var userClientResource = _mapper.Map<UserClientResource>(userClient);
                return Ok(userClientResource); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }
        
        [HttpDelete("{userClientId}/favorites/{bookId}")]
        [SwaggerOperation(
            Summary= "Eliminar un libro favorito de un UserClient previamente creado.",
            Description= "Elimina un libro favorito de un UserClient del sistema."
        )]
        public async Task<ActionResult<UserClientResource>> RemoveBookFromFavorites(int userClientId, int bookId)
        {
            var command = new RemoveFavoriteBookCommand(userClientId, bookId);
            try
            {
                var userClient = await _userClientCommandService.Handle(command);
                var userClientResource = _mapper.Map<UserClientResource>(userClient);
                return Ok(userClientResource); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }
        
        [HttpGet("{userClientId}/favorites")]
        [SwaggerOperation(
            Summary= "Obtener los datos de los favoritos que le pertenecen a un usuario en específico.",
            Description= "Te muestra los datos de los favoritos por medio del id de un usuario en específico."
        )]
        public async Task<ActionResult<IEnumerable<BookResource>>> GetUserFavoriteBooks(int userClientId)
        {
            var query = new GetUserClientByIdQuery(userClientId); 
            var userClient = await _userClientQueryService.Handle(query);

            if (userClient == null)
            {
                return NotFound(new { message = $"UserClient with ID {userClientId} not found." });
            }

            var favoriteBookResources = _mapper.Map<IEnumerable<BookResource>>(userClient.FavoriteBooks);
            return Ok(favoriteBookResources);
        }
    }
}