using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.commerce.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq; 

namespace LivriaBackend.commerce.Interfaces.REST.Controllers
{
    /// <summary>
    /// Controlador RESTful para gestionar las operaciones relacionadas con las reseñas de libros.
    /// </summary>
    [ApiController]
    [Route("api/v1/reviews")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewCommandService _reviewCommandService; 
        private readonly IReviewQueryService _reviewQueryService;
        private readonly IMapper _mapper;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ReviewsController"/>.
        /// </summary>
        /// <param name="reviewCommandService">El servicio de comandos de reseñas.</param>
        /// <param name="reviewQueryService">El servicio de consulta de reseñas.</param>
        /// <param name="mapper">La instancia de AutoMapper para la transformación de objetos.</param>

        public ReviewsController(
            IReviewCommandService reviewCommandService,
            IReviewQueryService reviewQueryService,
            IMapper mapper)
        {
            _reviewCommandService = reviewCommandService;
            _reviewQueryService = reviewQueryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva reseña en el sistema.
        /// </summary>
        /// <param name="resource">Los datos de la nueva reseña a crear.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene el <see cref="ReviewResource"/> de la reseña creada
        /// con un código 201 CreatedAtAction si la operación es exitosa.
        /// Retorna BadRequest (400) si hay un error de argumento (ej. ID de libro o usuario inválido).
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
            Summary= "Crear una nueva review.",
            Description= "Crea una nueva review en el sistema."
        )]
        [ProducesResponseType(typeof(ReviewResource), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReviewResource>> CreateReview([FromBody] CreateReviewResource resource)
        {
            var createCommand = _mapper.Map<CreateReviewCommand>(resource);

            Review review;
            try
            {
                review = await _reviewCommandService.Handle(createCommand);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            var reviewResource = _mapper.Map<ReviewResource>(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, reviewResource);
        }

        /// <summary>
        /// Obtiene los datos de todas las reseñas disponibles en el sistema.
        /// </summary>
        /// <returns>
        /// Una acción de resultado HTTP que contiene una colección de <see cref="ReviewResource"/>
        /// si la operación es exitosa (código 200 OK). Puede ser una colección vacía.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary= "Obtener los datos de todas las reseñas.", 
            Description= "Te muestra los datos de todas las reseñas."
            
        )]
        [ProducesResponseType(typeof(IEnumerable<ReviewResource>), 200)]
        public async Task<ActionResult<IEnumerable<ReviewResource>>> GetAllReviews()
        {
            var query = new GetAllReviewsQuery();
            var reviews = await _reviewQueryService.Handle(query);
            var reviewResources = _mapper.Map<IEnumerable<ReviewResource>>(reviews);
            return Ok(reviewResources);
        }

        /// <summary>
        /// Obtiene los datos de una reseña específica por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único de la reseña.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene un <see cref="ReviewResource"/> si la reseña es encontrada (código 200 OK),
        /// o un resultado NotFound (código 404) si la reseña no existe.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de una reseña en específico.", 
            Description= "Te muestra los datos de la reseña que buscaste."
        )]
        [ProducesResponseType(typeof(ReviewResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewResource>> GetReviewById(int id)
        {
            var query = new GetReviewByIdQuery(id);
            var review = await _reviewQueryService.Handle(query);

            if (review == null)
            {
                return NotFound();
            }

            var reviewResource = _mapper.Map<ReviewResource>(review);
            return Ok(reviewResource);
        }
        
        /// <summary>
        /// Obtiene todas las reseñas para un libro específico por su identificador único.
        /// </summary>
        /// <param name="bookId">El identificador único del libro.</param>
        /// <returns>
        /// Una acción de resultado HTTP que contiene una colección de <see cref="ReviewResource"/>
        /// para el libro especificado (código 200 OK). Puede ser una colección vacía si el libro no tiene reseñas.
        /// </returns>
        [HttpGet("book/{bookId}")] 
        [SwaggerOperation(
            Summary = "Obtener todas las reseñas para un libro específico.",
            Description = "Obtiene una lista de todas las reseñas asociadas con un ID de libro dado."
        )]
        [ProducesResponseType(typeof(IEnumerable<ReviewResource>), 200)]
        public async Task<ActionResult<IEnumerable<ReviewResource>>> GetReviewsByBookId(int bookId)
        {
            var query = new GetReviewsByBookIdQuery(bookId);
            var reviews = await _reviewQueryService.Handle(query);

            if (reviews == null) 
            {
                return Ok(Enumerable.Empty<ReviewResource>()); 
            }

            var resources = _mapper.Map<IEnumerable<ReviewResource>>(reviews);
            return Ok(resources);
        }
    }
}