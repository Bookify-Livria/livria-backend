using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.commerce.Domain.Model.Queries;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace LivriaBackend.commerce.Application.Internal.QueryServices
{
    public class BookQueryService : IBookQueryService
    {
        private readonly IBookRepository _bookRepository;

        public BookQueryService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdQuery query)
        {
           
            return await _bookRepository.GetByIdAsync(query.BookId); 
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery query)
        {
            
            return await _bookRepository.GetAllAsync(); 
        }
    }
}