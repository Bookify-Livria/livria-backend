using AutoMapper;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.commerce.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace LivriaBackend.commerce.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/cart-items")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemCommandService _cartItemCommandService;
        private readonly ICartItemQueryService _cartItemQueryService;
        private readonly IMapper _mapper;

        public CartItemsController(ICartItemCommandService cartItemCommandService, ICartItemQueryService cartItemQueryService, IMapper mapper)
        {
            _cartItemCommandService = cartItemCommandService;
            _cartItemQueryService = cartItemQueryService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary= "Crear un nuevo carrito.",
            Description= "Crea un nuevo carrito en el sistema."
        )]
        public async Task<ActionResult<CartItemResource>> CreateCartItem([FromBody] CreateCartItemResource resource)
        {
            var command = _mapper.Map<CreateCartItemCommand>(resource);
            try
            {
                var cartItem = await _cartItemCommandService.Handle(command);
                var cartItemResource = _mapper.Map<CartItemResource>(cartItem);
                return CreatedAtAction(nameof(GetCartItemById), new { id = cartItem.Id }, cartItemResource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPut("{id}/users/{userClientId}")]
        [SwaggerOperation(
            Summary= "Actualizar la cantidad de libros de un carrito existente.",
            Description= "Te permite modificar la cantidad de libros de un carrito previamente creado."
        )]
        public async Task<ActionResult<CartItemResource>> UpdateCartItemQuantity(int id, int userClientId, [FromBody] UpdateCartItemQuantityResource resource)
        {
            var command = new UpdateCartItemQuantityCommand(id, resource.NewQuantity, userClientId);
            try
            {
                var cartItem = await _cartItemCommandService.Handle(command);
                if (cartItem == null)
                {
                    return NotFound(); 
                }
                var cartItemResource = _mapper.Map<CartItemResource>(cartItem);
                return Ok(cartItemResource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}/users/{userClientId}")]
        [SwaggerOperation(
            Summary= "Eliminar un carrito de un UserClient previamente creado.",
            Description= "Elimina un carrito de un UserClient del sistema."
        )]
        public async Task<IActionResult> RemoveCartItem(int id, int userClientId)
        {
            var command = new RemoveCartItemCommand(id, userClientId);
            try
            {
                bool removed = await _cartItemCommandService.Handle(command);
                if (!removed)
                {
                    return NotFound(); 
                }
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary= "Obtener los datos de un libro perteneciente a un carrito en específico.",
            Description= "Te muestra los datos de un libro perteneciente al carrito que buscaste."
        )]
        public async Task<ActionResult<CartItemResource>> GetCartItemById(int id)
        {
            var query = new GetCartItemByIdQuery(id);
            var cartItem = await _cartItemQueryService.Handle(query);

            if (cartItem == null)
            {
                return NotFound();
            }

            var cartItemResource = _mapper.Map<CartItemResource>(cartItem);
            return Ok(cartItemResource);
        }

        [HttpGet("users/{userClientId}")]
        [SwaggerOperation(
            Summary= "Obtener los datos del carrito del usuario especificado.",
            Description= "Te muestra los datos del carrito del usuario especificado."
        )]
        public async Task<ActionResult<IEnumerable<CartItemResource>>> GetCartItemsByUserId(int userClientId)
        {
            var query = new GetAllCartItemsByUserIdQuery(userClientId);
            var cartItems = await _cartItemQueryService.Handle(query);
            var cartItemResources = _mapper.Map<IEnumerable<CartItemResource>>(cartItems);
            return Ok(cartItemResources);
        }
    }
}