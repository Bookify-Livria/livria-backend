using AutoMapper;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mime; 
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace LivriaBackend.communities.Interfaces.REST.Controllers
{
    /// <summary>
    /// Controlador RESTful para gestionar las operaciones relacionadas con las publicaciones (posts).
    /// </summary>
    [ApiController]
    [Route("api/v1/posts")] 
    [Produces(MediaTypeNames.Application.Json)]
    public class PostController : ControllerBase
    {
        private readonly IPostCommandService _postCommandService;
        private readonly IPostQueryService _postQueryService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PostController"/>.
        /// </summary>
        /// <param name="postCommandService">El servicio de comandos de publicaciones.</param>
        /// <param name="postQueryService">El servicio de consulta de publicaciones.</param>
        /// <param name="mapper">La instancia de AutoMapper para la transformación de objetos.</param>
        public PostController(IPostCommandService postCommandService, IPostQueryService postQueryService, IMapper mapper)
        {
            _postCommandService = postCommandService;
            _postQueryService = postQueryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva publicación en una comunidad existente.
        /// </summary>
        /// <param name="communityId">El identificador único de la comunidad a la que pertenecerá la publicación.</param>
        /// <param name="resource">El recurso que contiene los datos de la publicación a crear.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene el <see cref="PostResource"/> de la publicación creada
        /// con un código 201 CreatedAtAction si la operación es exitosa.
        /// Retorna BadRequest (400) si la publicación no pudo ser creada o si la comunidad/usuario no existen.
        /// </returns>
        [HttpPost("communities/{communityId}")] 
        [SwaggerOperation(
            Summary= "Crear una nueva publicación en una comunidad existente.",
            Description= "Crea una nueva publicación en una comunidad existente en el sistema."
        )]
        [ProducesResponseType(typeof(PostResource), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PostResource>> CreatePost(int communityId, [FromBody] CreatePostResource resource)
        {
            var createPostCommand = new CreatePostCommand(
                communityId,
                resource.Username, 
                resource.Content,
                resource.Img
            );

            Post post;
            try
            {
                post = await _postCommandService.Handle(createPostCommand);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
            
            if (post == null)
            {
                // Este caso debería ser cubierto por las excepciones en el servicio, pero se mantiene como un fallback.
                return BadRequest(new { message = "Could not create post." });
            }

            var postResource = _mapper.Map<PostResource>(post);
            
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, postResource);
        }

        /// <summary>
        /// Obtiene los datos de una publicación específica por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único de la publicación.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene un <see cref="PostResource"/> si la publicación es encontrada (código 200 OK),
        /// o un resultado NotFound (código 404) si la publicación no existe.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de una publicación en específico.",
            Description= "Te muestra los datos de la publicación que buscaste."
        )]
        [ProducesResponseType(typeof(PostResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PostResource>> GetPostById(int id)
        {
            var post = await _postQueryService.Handle(new GetPostByIdQuery(id));
            if (post == null)
            {
                return NotFound();
            }
            var postResource = _mapper.Map<PostResource>(post);
            return Ok(postResource);
        }

        /// <summary>
        /// Obtiene los datos de todas las publicaciones disponibles en el sistema.
        /// </summary>
        /// <returns>
        /// Una acción de resultado HTTP que contiene una colección de <see cref="PostResource"/>
        /// si la operación es exitosa (código 200 OK). Puede ser una colección vacía si no hay publicaciones.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary= "Obtener los datos de todas las publicaciones.",
            Description= "Te muestra los datos de las publicaciones."
            
        )]
        [ProducesResponseType(typeof(IEnumerable<PostResource>), 200)]
        public async Task<ActionResult<IEnumerable<PostResource>>> GetAllPosts()
        {
            var posts = await _postQueryService.Handle(new GetAllPostsQuery());
            var postResources = _mapper.Map<IEnumerable<PostResource>>(posts);
            return Ok(postResources);
        }
        
        /// <summary>
        /// Obtiene todas las publicaciones para una comunidad específica por su identificador único.
        /// </summary>
        /// <param name="communityId">El identificador único de la comunidad.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene una colección de <see cref="PostResource"/>
        /// para la comunidad especificada (código 200 OK). Puede ser una colección vacía si la comunidad no tiene publicaciones.
        /// </returns>
        [HttpGet("community/{communityId}")] 
        [SwaggerOperation(
            Summary = "Obtener todas las publicaciones para una comunidad específica.",
            Description = "Obtiene una lista de todas las publicaciones asociadas con un ID de comunidad dado."
        )]
        [ProducesResponseType(typeof(IEnumerable<PostResource>), 200)]
        public async Task<ActionResult<IEnumerable<PostResource>>> GetPostsByCommunityId(int communityId)
        {
            var query = new GetPostsByCommunityIdQuery(communityId);
            var posts = await _postQueryService.Handle(query);

            if (posts == null) 
            {
                return Ok(Enumerable.Empty<PostResource>()); 
            }

            var resources = _mapper.Map<IEnumerable<PostResource>>(posts);
            return Ok(resources);
        }
    }
}