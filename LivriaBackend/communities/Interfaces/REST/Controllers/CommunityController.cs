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
        private readonly IUserCommunityCommandService _userCommunityCommandService; 
        private readonly IMapper _mapper;

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

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommunityResource>>> GetAllCommunities()
        {
            var query = new GetAllCommunitiesQuery();
            var communities = await _communityQueryService.Handle(query);
            var communityResources = _mapper.Map<IEnumerable<Community>, IEnumerable<CommunityResource>>(communities);
            return Ok(communityResources);
        }


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


        [HttpPost("join")]
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