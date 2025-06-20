using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.QueryServices
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueryService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(GetOrderByIdQuery query)
        {
            return await _orderRepository.GetByIdAsync(query.OrderId);
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersByUserIdQuery query)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(query.UserClientId);
        }

        public async Task<Order> Handle(GetOrderByCodeQuery query)
        {
            return await _orderRepository.GetOrderByCodeAsync(query.OrderCode);
        }
    }
}