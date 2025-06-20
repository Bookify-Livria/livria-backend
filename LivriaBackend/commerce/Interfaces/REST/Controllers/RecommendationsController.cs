using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.commerce.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using System;
using LivriaBackend.commerce.Domain.Model.Aggregates;

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