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
    public class CartItemCommandService : ICartItemCommandService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IBookRepository _bookRepository; 
        private readonly IUserClientRepository _userClientRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public CartItemCommandService(ICartItemRepository cartItemRepository, IBookRepository bookRepository, IUserClientRepository userClientRepository, IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _bookRepository = bookRepository;
            _userClientRepository = userClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartItem> Handle(CreateCartItemCommand command)
        {
            var book = await _bookRepository.GetByIdAsync(command.BookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with ID {command.BookId} not found.");
            }

            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {command.UserClientId} not found.");
            }

            var existingCartItem = await _cartItemRepository.FindByBookAndUserAsync(command.BookId, command.UserClientId);

            if (existingCartItem != null)
            {
                existingCartItem.UpdateQuantity(existingCartItem.Quantity + command.Quantity);
                await _cartItemRepository.UpdateAsync(existingCartItem);
                await _unitOfWork.CompleteAsync();
                return existingCartItem;
            }
            else
            {
                var cartItem = new CartItem(command.BookId, command.Quantity, command.UserClientId);
                await _cartItemRepository.AddAsync(cartItem);
                await _unitOfWork.CompleteAsync();
                return cartItem;
            }
        }

        public async Task<CartItem> Handle(UpdateCartItemQuantityCommand command)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(command.CartItemId);

            if (cartItem == null || cartItem.UserClientId != command.UserClientId)
            {
                throw new ArgumentException($"CartItem with ID {command.CartItemId} not found or does not belong to UserClient {command.UserClientId}.");
            }

            if (command.NewQuantity == 0)
            {
                await _cartItemRepository.DeleteAsync(cartItem);
            }
            else
            {
                cartItem.UpdateQuantity(command.NewQuantity);
                await _cartItemRepository.UpdateAsync(cartItem);
            }

            await _unitOfWork.CompleteAsync();
            return cartItem;
        }

        public async Task<bool> Handle(RemoveCartItemCommand command)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(command.CartItemId);

            if (cartItem == null || cartItem.UserClientId != command.UserClientId)
            {
                return false; 
            }

            await _cartItemRepository.DeleteAsync(cartItem);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}