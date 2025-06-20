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

namespace LivriaBackend.communities.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/communities")]
    public class CommunitiesController : ControllerBase
    {
        private readonly ICommunityCommandService _communityCommandService;
        private readonly ICommunityQueryService _communityQueryService;
        private readonly IUserCommunityCommandService _userCommunityCommandService; // NEW
        private readonly IMapper _mapper;

        public CommunitiesController(
            ICommunityCommandService communityCommandService,
            ICommunityQueryService communityQueryService,
            IUserCommunityCommandService userCommunityCommandService, // NEW
            IMapper mapper)
        {
            _communityCommandService = communityCommandService;
            _communityQueryService = communityQueryService;
            _userCommunityCommandService = userCommunityCommandService; // NEW
            _mapper = mapper;
        }

        // POST: api/v1/communities
        /// <summary>
        /// Creates a new community.
        /// </summary>
        /// <param name="resource">Resource with the new community data.</param>
        /// <returns>The created community resource with a 201 Created code.</returns>
        [HttpPost]
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

        // GET: api/v1/communities
        /// <summary>
        /// Gets all communities.
        /// </summary>
        /// <returns>A list of community resources.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommunityResource>>> GetAllCommunities()
        {
            var query = new GetAllCommunitiesQuery();
            var communities = await _communityQueryService.Handle(query);
            var communityResources = _mapper.Map<IEnumerable<Community>, IEnumerable<CommunityResource>>(communities);
            return Ok(communityResources);
        }

        // GET: api/v1/communities/{id}
        /// <summary>
        /// Gets a community by its ID.
        /// </summary>
        /// <param name="id">ID of the community.</param>
        /// <returns>The community resource if found; otherwise, 404 Not Found.</returns>
        [HttpGet("{id}")]
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

        // NEW: Endpoint to join a community
        // POST: api/v1/communities/join
        /// <summary>
        /// Allows a UserClient to join a Community.
        /// </summary>
        /// <param name="resource">Resource containing UserClient ID and Community ID.</param>
        /// <returns>The UserCommunity resource with code 201 Created.</returns>
        [HttpPost("join")]
        public async Task<ActionResult<UserCommunityResource>> JoinCommunity([FromBody] JoinCommunityResource resource)
        {
            var command = _mapper.Map<JoinCommunityResource, JoinCommunityCommand>(resource);
            try
            {
                var userCommunity = await _userCommunityCommandService.Handle(command);
                var userCommunityResource = _mapper.Map<UserCommunity, UserCommunityResource>(userCommunity);
                return CreatedAtAction(nameof(JoinCommunity), userCommunityResource); // You might want to adjust the route parameter if needed.
            }
            catch (ApplicationException ex)
            {
                // Handle specific application exceptions (e.g., user/community not found, already joined)
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while trying to join the community.");
            }
        }
    }
}