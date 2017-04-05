using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Database.Dapper.Models;

namespace Business.Services
{
    public class BooksService : IBooksService
    {
        private readonly IMapper _mapper;
        private readonly IBooksGateway _booksGateway;

        public BooksService(IMapper mapper, IBooksGateway booksGateway)
        {
            _mapper = mapper;
            _booksGateway = booksGateway;
        }

        public async Task<IEnumerable<Book>> Get(BookFilter filter)
        {
            var dataFilter = _mapper.Map<BookFilter, BookDataFilter>(filter);
            var booksData = await _booksGateway.Get(dataFilter);
            var books = _mapper.Map<IEnumerable<BookData>, IEnumerable<Book>>(booksData);
            return books;
        }

        public async Task<Book> GetSingle(int id)
        {
            var bookData = await _booksGateway.GetSingle(id);
            var book = _mapper.Map<BookData, Book>(bookData);
            return book;
        }

        public async Task<int> Add(Book book)
        {
            var bookData = _mapper.Map<Book, BookData>(book);
            return await _booksGateway.Add(bookData);
        }

        public async Task<bool> Update(Book book)
        {
            var bookData = _mapper.Map<Book, BookData>(book);
            return await _booksGateway.Update(bookData);
        }

        public async Task<bool> Delete(Book book)
        {
            var bookData = _mapper.Map<Book, BookData>(book);
            return await _booksGateway.Delete(bookData);
        }
    }
}