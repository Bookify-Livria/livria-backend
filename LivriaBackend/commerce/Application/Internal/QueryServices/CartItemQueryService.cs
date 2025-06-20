using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.QueryServices
{
    public class CartItemQueryService : ICartItemQueryService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemQueryService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItem> Handle(GetCartItemByIdQuery query)
        {
            return await _cartItemRepository.GetByIdAsync(query.CartItemId);
        }

        public async Task<IEnumerable<CartItem>> Handle(GetAllCartItemsByUserIdQuery query)
        {
            return await _cartItemRepository.GetCartItemsByUserIdAsync(query.UserClientId);
        }
    }
}