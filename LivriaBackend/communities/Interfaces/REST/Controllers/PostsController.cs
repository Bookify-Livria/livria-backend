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
    [ApiController]
    [Route("api/v1/posts")] 
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

        
        [HttpPost("communities/{communityId}")] 
        [SwaggerOperation(
            Summary= "Crear una nueva pubicación en una comunidad existente.",
            Description= "Crea una nueva pubicación en una comunidad existente en el sistema."
        )]
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
                
                return BadRequest(new { message = "Could not create post." });
            }

            
            var postResource = _mapper.Map<PostResource>(post);
            
            
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, postResource);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de una publicación en específico.",
            Description= "Te muestra los datos de la publicación que buscaste."
        )]
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
        [SwaggerOperation(
            Summary= "Obtener los datos de todas las publicaciones.",
            Description= "Te muestra los datos de las publicaciones."
            
        )]
        public async Task<ActionResult<IEnumerable<PostResource>>> GetAllPosts()
        {
            var posts = await _postQueryService.Handle(new GetAllPostsQuery());
            var postResources = _mapper.Map<IEnumerable<PostResource>>(posts);
            return Ok(postResources);
        }

        
    }
}