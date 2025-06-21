using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Interfaces.REST.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime; 

namespace LivriaBackend.communities.Interfaces.REST.Controllers
{
    /// <summary>
    /// Controlador RESTful para gestionar las operaciones relacionadas con las comunidades.
    /// </summary>
    [ApiController]
    [Route("api/v1/communities")]
    [Produces(MediaTypeNames.Application.Json)] 
    public class CommunitiesController : ControllerBase
    {
        private readonly ICommunityCommandService _communityCommandService;
        private readonly ICommunityQueryService _communityQueryService;
        private readonly IUserCommunityCommandService _userCommunityCommandService; 
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CommunitiesController"/>.
        /// </summary>
        /// <param name="communityCommandService">El servicio de comandos de comunidades.</param>
        /// <param name="communityQueryService">El servicio de consulta de comunidades.</param>
        /// <param name="userCommunityCommandService">El servicio de comandos para relaciones usuario-comunidad.</param>
        /// <param name="mapper">La instancia de AutoMapper para la transformación de objetos.</param>
        public CommunitiesController(
            ICommunityCommandService communityCommandService,
            ICommunityQueryService communityQueryService,
            IUserCommunityCommandService userCommunityCommandService, 
            IMapper mapper)
        {
            _communityCommandService = communityCommandService;
            _communityQueryService = communityQueryService;
            _userCommunityCommandService = userCommunityCommandService; 
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva comunidad en el sistema.
        /// </summary>
        /// <param name="resource">El recurso que contiene los datos de la comunidad a crear.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene el <see cref="CommunityResource"/> de la comunidad creada
        /// con un código 201 CreatedAtAction si la operación es exitosa.
        /// Retorna BadRequest (400) si la comunidad no pudo ser creada debido a datos inválidos.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
            Summary= "Crear una nueva comunidad.",
            Description= "Crea una nueva comunidad en el sistema."
        )]
        [ProducesResponseType(typeof(CommunityResource), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CommunityResource>> CreateCommunity([FromBody] CreateCommunityResource resource)
        {
            var command = _mapper.Map<CreateCommunityResource, CreateCommunityCommand>(resource);
            var community = await _communityCommandService.Handle(command);

            if (community == null)
            {
                return BadRequest("Could not create community. Check provided data.");
            }

            var communityResource = _mapper.Map<Community, CommunityResource>(community);
            return CreatedAtAction(nameof(GetCommunityById), new { id = communityResource.Id }, communityResource);
        }

        /// <summary>
        /// Obtiene los datos de todas las comunidades disponibles en el sistema.
        /// </summary>
        /// <returns>
        /// Una acción de resultado HTTP que contiene una colección de <see cref="CommunityResource"/>
        /// si la operación es exitosa (código 200 OK). Puede ser una colección vacía si no hay comunidades.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary= "Obtener los datos de todas las comunidades.",
            Description= "Te muestra los datos de las comunidades."
            
        )]
        [ProducesResponseType(typeof(IEnumerable<CommunityResource>), 200)]
        public async Task<ActionResult<IEnumerable<CommunityResource>>> GetAllCommunities()
        {
            var query = new GetAllCommunitiesQuery();
            var communities = await _communityQueryService.Handle(query);
            var communityResources = _mapper.Map<IEnumerable<Community>, IEnumerable<CommunityResource>>(communities);
            return Ok(communityResources);
        }

        /// <summary>
        /// Obtiene los datos de una comunidad específica por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único de la comunidad.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene un <see cref="CommunityResource"/> si la comunidad es encontrada (código 200 OK),
        /// o un resultado NotFound (código 404) si la comunidad no existe.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de una comunidad en específico.",
            Description= "Te muestra los datos de la comunidad que buscaste."
        )]
        [ProducesResponseType(typeof(CommunityResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CommunityResource>> GetCommunityById(int id)
        {
            var query = new GetCommunityByIdQuery(id);
            var community = await _communityQueryService.Handle(query);

            if (community == null)
            {
                return NotFound();
            }

            var communityResource = _mapper.Map<Community, CommunityResource>(community);
            return Ok(communityResource);
        }

        /// <summary>
        /// Permite a un cliente de usuario unirse a una comunidad existente.
        /// </summary>
        /// <param name="resource">El recurso que contiene los IDs del cliente de usuario y la comunidad.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene el <see cref="UserCommunityResource"/> de la membresía creada
        /// con un código 201 CreatedAtAction si la operación es exitosa.
        /// Retorna BadRequest (400) si la unión no es posible (ej. usuario o comunidad no encontrados, suscripción inválida, ya es miembro).
        /// Retorna StatusCode 500 si ocurre un error inesperado.
        /// </returns>
        [HttpPost("join")]
        [SwaggerOperation(
            Summary= "Unirse una comunidad existente.",
            Description= "El userclient puede unirse a una comunidad existente."
        )]
        [ProducesResponseType(typeof(UserCommunityResource), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserCommunityResource>> JoinCommunity([FromBody] JoinCommunityResource resource)
        {
            var command = _mapper.Map<JoinCommunityResource, JoinCommunityCommand>(resource);
            try
            {
                var userCommunity = await _userCommunityCommandService.Handle(command);
                var userCommunityResource = _mapper.Map<UserCommunity, UserCommunityResource>(userCommunity);
                return CreatedAtAction(nameof(JoinCommunity), userCommunityResource);
            }
            catch (ApplicationException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while trying to join the community.");
            }
        }
    }
}