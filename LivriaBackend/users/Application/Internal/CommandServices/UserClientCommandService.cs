using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System;
using System.Threading.Tasks;
using LivriaBackend.commerce.Domain.Repositories;

namespace LivriaBackend.users.Application.Internal.CommandServices
{
    public class UserClientCommandService : IUserClientCommandService
    {
        private readonly IUserClientRepository _userClientRepository;
        private readonly IBookRepository _bookRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public UserClientCommandService(IUserClientRepository userClientRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _userClientRepository = userClientRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserClient> Handle(CreateUserClientCommand command)
        {
            if (await _userClientRepository.ExistsByUsernameAsync(command.Username))
            {
                throw new ArgumentException($"User with username '{command.Username}' already exists.");
            }
            if (await _userClientRepository.ExistsByEmailAsync(command.Email))
            {
                throw new ArgumentException($"User with email '{command.Email}' already exists.");
            }

            var userClient = new UserClient(command.Display, command.Username, command.Email, command.Password, command.Icon, command.Phrase, command.Subscription);
            await _userClientRepository.AddAsync(userClient);
            await _unitOfWork.CompleteAsync();
            return userClient;
        }

        public async Task<UserClient> Handle(UpdateUserClientCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {command.UserClientId} not found.");
            }

      

            await _userClientRepository.UpdateAsync(userClient);
            await _unitOfWork.CompleteAsync();
            return userClient;
        }

        public async Task<bool> Handle(DeleteUserClientCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                return false;
            }

            await _userClientRepository.DeleteAsync(userClient);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<UserClient> Handle(AddFavoriteBookCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {command.UserClientId} not found.");
            }

            var book = await _bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with ID {command.BookId} not found.");
            }

            if (userClient.FavoriteBooks.Any(fb => fb.Id == book.Id))
            {
                throw new InvalidOperationException($"Book with ID {book.Id} is already in favorites for UserClient {userClient.Id}.");
            }

            userClient.AddFavoriteBook(book); 
            await _userClientRepository.UpdateAsync(userClient); 
            await _unitOfWork.CompleteAsync(); 

            return userClient;
        }

        public async Task<UserClient> Handle(RemoveFavoriteBookCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {command.UserClientId} not found.");
            }

            var book = await _bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with ID {command.BookId} not found.");
            }

            if (!userClient.FavoriteBooks.Any(fb => fb.Id == book.Id))
            {
                throw new InvalidOperationException($"Book with ID {book.Id} is not in favorites for UserClient {userClient.Id}.");
            }

            userClient.RemoveFavoriteBook(book); 
            await _userClientRepository.UpdateAsync(userClient); 
            await _unitOfWork.CompleteAsync(); 

            return userClient;
        }
    }
}