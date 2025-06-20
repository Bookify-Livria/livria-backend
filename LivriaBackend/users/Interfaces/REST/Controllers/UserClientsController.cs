using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Interfaces.REST.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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
        public async Task<ActionResult<UserClientResource>> CreateUserClient([FromBody] CreateUserClientResource resource)
        {
            var command = _mapper.Map<CreateUserClientCommand>(resource);
            var userClient = await _userClientCommandService.Handle(command);
            var userClientResource = _mapper.Map<UserClientResource>(userClient);
            return CreatedAtAction(nameof(GetUserClientById), new { id = userClientResource.Id }, userClientResource);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserClientResource>>> GetAllUserClients()
        {
            var query = new GetAllUserClientQuery();
            var userClients = await _userClientQueryService.Handle(query);
            var userClientResources = _mapper.Map<IEnumerable<UserClientResource>>(userClients);
            return Ok(userClientResources);
        }

        [HttpGet("{id}")]
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
    }
}