using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
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
    [Route("api/v1/orders")]
    [Produces(MediaTypeNames.Application.Json)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderCommandService _orderCommandService;
        private readonly IOrderQueryService _orderQueryService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderCommandService orderCommandService, IOrderQueryService orderQueryService, IMapper mapper)
        {
            _orderCommandService = orderCommandService;
            _orderQueryService = orderQueryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResource>> CreateOrder([FromBody] CreateOrderResource resource)
        {
            
            var command = _mapper.Map<CreateOrderCommand>(resource);
            try
            {
                var order = await _orderCommandService.Handle(command);
                var orderResource = _mapper.Map<OrderResource>(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderResource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResource>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _orderQueryService.Handle(query);

            if (order == null)
            {
                return NotFound();
            }

            var orderResource = _mapper.Map<OrderResource>(order);
            return Ok(orderResource);
        }

        [HttpGet("code/{code}")]
        public async Task<ActionResult<OrderResource>> GetOrderByCode(string code)
        {
            var query = new GetOrderByCodeQuery(code);
            var order = await _orderQueryService.Handle(query);

            if (order == null)
            {
                return NotFound();
            }

            var orderResource = _mapper.Map<OrderResource>(order);
            return Ok(orderResource);
        }


        [HttpGet("users/{userClientId}")]
        public async Task<ActionResult<IEnumerable<OrderResource>>> GetOrdersByUserId(int userClientId)
        {
            var query = new GetOrdersByUserIdQuery(userClientId);
            var orders = await _orderQueryService.Handle(query);
            var orderResources = _mapper.Map<IEnumerable<OrderResource>>(orders);
            return Ok(orderResources);
        }
    }
}