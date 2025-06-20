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
    [Route("api/v1/useradmins")]
    public class UserAdminsController : ControllerBase
    {
        private readonly IUserAdminCommandService _userAdminCommandService;
        private readonly IUserAdminQueryService _userAdminQueryService;
        private readonly IMapper _mapper;

        public UserAdminsController(
            IUserAdminCommandService userAdminCommandService,
            IUserAdminQueryService userAdminQueryService,
            IMapper mapper)
        {
            _userAdminCommandService = userAdminCommandService;
            _userAdminQueryService = userAdminQueryService;
            _mapper = mapper;
        }

        // REMOVIDO: No hay endpoint POST para crear UserAdmin

        // GET: api/v1/useradmins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdminResource>>> GetAllUserAdmins()
        {
            var query = new GetAllUserAdminQuery();
            var userAdmins = await _userAdminQueryService.Handle(query);
            var userAdminResources = _mapper.Map<IEnumerable<UserAdminResource>>(userAdmins);
            return Ok(userAdminResources);
        }


        // PUT: api/v1/useradmins/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UserAdminResource>> UpdateUserAdmin(int id, [FromBody] UpdateUserAdminResource resource)
        {
            var command = new UpdateUserAdminCommand(
                id,
                resource.Display,
                resource.Username,
                resource.Email,
                resource.Password,
                resource.AdminAccess,
                resource.SecurityPin
            );

            try
            {
                var updatedUserAdmin = await _userAdminCommandService.Handle(command);
                var userAdminResource = _mapper.Map<UserAdminResource>(updatedUserAdmin);
                return Ok(userAdminResource);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while updating the user admin.");
            }
        }

        // REMOVIDO: No hay endpoint DELETE para eliminar UserAdmin
    }
}