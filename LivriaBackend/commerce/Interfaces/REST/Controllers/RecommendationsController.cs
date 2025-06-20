using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.commerce.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using System;
using LivriaBackend.commerce.Domain.Model.Aggregates;
<<<<<<< HEAD
=======
using Swashbuckle.AspNetCore.Annotations;
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d

namespace LivriaBackend.commerce.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/recommendations")]
    [Produces(MediaTypeNames.Application.Json)]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationQueryService _recommendationQueryService;
        private readonly IMapper _mapper;

        public RecommendationsController(IRecommendationQueryService recommendationQueryService, IMapper mapper)
        {
            _recommendationQueryService = recommendationQueryService;
            _mapper = mapper;
        }

        
        [HttpGet("users/{userClientId}")]
<<<<<<< HEAD
=======
        [SwaggerOperation(
            Summary= "Obtener los datos de las recomendaciones que le pertenecen a un usuario en específico.",
            Description= "Te muestra los datos de las recomendaciones por medio del id de un usuario en específico."
        )]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        public async Task<ActionResult<RecommendationResource>> GetUserRecommendations(int userClientId)
        {
            var query = new GetUserRecommendationsQuery(userClientId);
            try
            {
                var recommendation = await _recommendationQueryService.Handle(query);
                if (recommendation == null || !recommendation.RecommendedBooks.Any())
                {
                    return Ok(_mapper.Map<RecommendationResource>(recommendation ?? new Domain.Model.Entities.Recommendation(userClientId, new System.Collections.Generic.List<Book>())));
                }
                var resource = _mapper.Map<RecommendationResource>(recommendation);
                return Ok(resource);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }
    }
}