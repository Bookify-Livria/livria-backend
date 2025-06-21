using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Model.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Model.Services
{
    public interface IReviewQueryService
    {
        Task<Review> Handle(GetReviewByIdQuery query);
        Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query);
        
        
        Task<IEnumerable<Review>> Handle(GetReviewsByBookIdQuery query);
    }
}