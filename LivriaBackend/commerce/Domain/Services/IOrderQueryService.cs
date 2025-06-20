using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Model.Services
{
    public interface IOrderQueryService
    {
        Task<Order> Handle(GetOrderByIdQuery query);
        Task<IEnumerable<Order>> Handle(GetOrdersByUserIdQuery query);
        Task<Order> Handle(GetOrderByCodeQuery query);
    }
}