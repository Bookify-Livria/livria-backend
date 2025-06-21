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

namespace LivriaBackend.commerce.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/reviews")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewCommandService _reviewCommandService;
        private readonly IReviewQueryService _reviewQueryService;
        private readonly IMapper _mapper;

        public ReviewsController(
            IReviewCommandService reviewCommandService,
            IReviewQueryService reviewQueryService,
            IMapper mapper)
        {
            _reviewCommandService = reviewCommandService;
            _reviewQueryService = reviewQueryService;
            _mapper = mapper;
        }

        [HttpPost]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResource>>> GetAllReviews()
        {
            var query = new GetAllReviewsQuery();
            var reviews = await _reviewQueryService.Handle(query);
            var reviewResources = _mapper.Map<IEnumerable<ReviewResource>>(reviews);
            return Ok(reviewResources);
        }

        [HttpGet("{id}")]
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
    }
}