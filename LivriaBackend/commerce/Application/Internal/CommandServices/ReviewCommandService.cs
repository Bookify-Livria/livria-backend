using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.users.Domain.Model.Repositories; 
using System;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.CommandServices
{
    public class ReviewCommandService : IReviewCommandService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserClientRepository _userClientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewCommandService(IReviewRepository reviewRepository, IBookRepository bookRepository, IUserClientRepository userClientRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _userClientRepository = userClientRepository; 
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> Handle(CreateReviewCommand command)
        {
            var book = await _bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with ID {command.BookId} not found for review.");
            }

            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {command.UserClientId} not found. Cannot create review.");
            }

            var usernameForReview = userClient.Display; 

            var review = new Review(command.BookId, command.UserClientId, command.Content, command.Stars, usernameForReview);

            await _reviewRepository.AddAsync(review);
            await _unitOfWork.CompleteAsync(); 

            return review;
        }
    }
}