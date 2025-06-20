using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Commands;
using LivriaBackend.commerce.Domain.Model.Entities; 
using LivriaBackend.commerce.Domain.Repositories; 
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories; 
using LivriaBackend.users.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.commerce.Application.Internal.CommandServices
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository; 
        private readonly IBookRepository _bookRepository; 
        private readonly IUserClientRepository _userClientRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public OrderCommandService(
            IOrderRepository orderRepository,
            ICartItemRepository cartItemRepository,
            IBookRepository bookRepository,
            IUserClientRepository userClientRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _bookRepository = bookRepository;
            _userClientRepository = userClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> Handle(CreateOrderCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with ID {command.UserClientId} not found.");
            }

            var cartItems = new List<CartItem>();
            foreach (var cartItemId in command.CartItemIds)
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null || cartItem.UserClientId != command.UserClientId)
                {
                    throw new ArgumentException($"CartItem with ID {cartItemId} not found or does not belong to UserClient {command.UserClientId}.");
                }
                cartItems.Add(cartItem);
            }

            if (!cartItems.Any())
            {
                throw new ArgumentException("Cannot create an order with an empty cart.");
            }

            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cartItems)
            {
                var book = await _bookRepository.GetByIdAsync(cartItem.BookId);
                if (book == null)
                {
                    throw new ArgumentException($"Book with ID {cartItem.BookId} for CartItem {cartItem.Id} not found.");
                }

                if (book.Stock < cartItem.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for book '{book.Title}'. Available: {book.Stock}, Requested: {cartItem.Quantity}.");
                }

                var orderItem = new OrderItem(
                    book.Id,
                    book.Title,
                    book.Author,
                    book.Price,
                    book.Cover,
                    cartItem.Quantity
                );
                orderItems.Add(orderItem);

                book.DecreaseStock(cartItem.Quantity);
                await _bookRepository.UpdateAsync(book);
            }

            var order = new Order(
                command.UserClientId,
                command.UserEmail,
                command.UserPhone,
                command.UserFullName,
                command.IsDelivery,
                command.ShippingDetails,
                orderItems 
            );

            await _orderRepository.AddAsync(order);

            foreach (var cartItem in cartItems)
            {
                await _cartItemRepository.DeleteAsync(cartItem);
            }

            await _unitOfWork.CompleteAsync(); 

            return order;
        }
    }
}