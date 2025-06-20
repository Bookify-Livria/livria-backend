using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.users.Domain.Model.Repositories;
using System;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Domain.Model.Services
{
    public interface IReviewCommandService
    {
        Task<Review> Handle(CreateReviewCommand command);
    }
}