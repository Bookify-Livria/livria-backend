using LivriaBackend.commerce.Domain.Model.Aggregates; 
using LivriaBackend.commerce.Domain.Model.Entities;    
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Repositories; 
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.users.Domain.Model.Repositories; 
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.QueryServices
{
    public class RecommendationQueryService : IRecommendationQueryService
    {
        private readonly IUserClientRepository _userClientRepository;
        private readonly IBookRepository _bookRepository;

        public RecommendationQueryService(IUserClientRepository userClientRepository, IBookRepository bookRepository)
        {
            _userClientRepository = userClientRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Recommendation> Handle(GetUserRecommendationsQuery query)
        {
            
            var userClient = await _userClientRepository.GetByIdAsync(query.UserClientId);

            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {query.UserClientId} not found.");
            }

            
            var favoriteGenres = userClient.FavoriteBooks
                                           .Select(b => b.Genre)
                                           .Where(g => !string.IsNullOrWhiteSpace(g))
                                           .Distinct()
                                           .ToList();

            if (!favoriteGenres.Any())
            {
               
                return new Recommendation(query.UserClientId, new List<Book>());
            }

           
            var allBooks = await _bookRepository.GetAllAsync(); 

            var recommendedBooks = allBooks
                .Where(book => favoriteGenres.Contains(book.Genre) && !userClient.FavoriteBooks.Any(fb => fb.Id == book.Id))
                .ToList();

            return new Recommendation(query.UserClientId, recommendedBooks);
        }
    }
}