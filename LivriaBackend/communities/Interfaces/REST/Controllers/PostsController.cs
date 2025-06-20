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
using System.Net.Mime; // Para MediaTypeNames
using System; // Para ArgumentException

namespace LivriaBackend.communities.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/posts")] // Ruta base del controlador de posts
    [Produces(MediaTypeNames.Application.Json)]
    public class PostController : ControllerBase
    {
        private readonly IPostCommandService _postCommandService;
        private readonly IPostQueryService _postQueryService;
        private readonly IMapper _mapper;

        public PostController(IPostCommandService postCommandService, IPostQueryService postQueryService, IMapper mapper)
        {
            _postCommandService = postCommandService;
            _postQueryService = postQueryService;
            _mapper = mapper;
        }

        // Endpoint para crear un Post dentro de una Comunidad
        [HttpPost("communities/{communityId}")] // La comunidad se especifica en la URL
        public async Task<ActionResult<PostResource>> CreatePost(int communityId, [FromBody] CreatePostResource resource)
        {
            // Mapea el recurso de la petición a un comando.
            // communityId viene de la URL, username, content, img del cuerpo.
            var createPostCommand = new CreatePostCommand(
                communityId,
                resource.Username, // El username viene directamente del recurso
                resource.Content,
                resource.Img
            );

            Post post;
            try
            {
                // Envía el comando al servicio para manejar la creación del post
                post = await _postCommandService.Handle(createPostCommand);
            }
            catch (ArgumentException ex) // Captura errores de validación del servicio (ej. comunidad/usuario no encontrado)
            {
                return BadRequest(new { message = ex.Message });
            }
            
            if (post == null)
            {
                // Esto podría ocurrir si el servicio decide no crear el post por alguna razón no manejada por ArgumentException
                return BadRequest(new { message = "Could not create post." });
            }

            // Mapea la entidad Post creada de vuelta a un recurso para la respuesta
            var postResource = _mapper.Map<PostResource>(post);
            
            // Retorna un código 201 Created junto con la URL del nuevo recurso y el recurso mismo
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, postResource);
        }

        [HttpGet("{id}")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResource>>> GetAllPosts()
        {
            var posts = await _postQueryService.Handle(new GetAllPostsQuery());
            var postResources = _mapper.Map<IEnumerable<PostResource>>(posts);
            return Ok(postResources);
        }

        // Puedes añadir aquí otros endpoints como PUT o DELETE si los implementas
    }
}