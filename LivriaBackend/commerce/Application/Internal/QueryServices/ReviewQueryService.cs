using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LivriaBackend.commerce.Application.Internal.QueryServices
{
    public class ReviewQueryService : IReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewQueryService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> Handle(GetReviewByIdQuery query)
        {
            
            return await _reviewRepository.GetByIdAsync(query.ReviewId);
        }

        public async Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query)
        {
           
            return await _reviewRepository.GetAllAsync();
        }
    }
}