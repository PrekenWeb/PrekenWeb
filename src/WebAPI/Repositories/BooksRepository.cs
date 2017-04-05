using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using WebAPI.Common;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    internal class BooksRepository : IBooksRepository
    {
        private readonly IBooksService _booksService;
        private readonly IMapper _mapper;

        public BooksRepository(IBooksService booksService, IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookViewModel>> Get(BookFilterModel filterModel)
        {
            var filter = _mapper.Map<BookFilter>(filterModel);
            var books = await _booksService.Get(filter);
            return _mapper.Map<IEnumerable<BookViewModel>>(books);
        }

        public async Task<BookViewModel> GetSingle(int id)
        {
            var book = await _booksService.GetSingle(id);
            if (book == null) throw new ItemNotFoundException();
            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<int> Add(BookViewModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            return await _booksService.Add(book);
        }

        public async Task<bool> Update(BookViewModel bookModel)
        {
            var existing = _booksService.GetSingle(bookModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var book = _mapper.Map<Book>(bookModel);
            return await _booksService.Update(book);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _booksService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _booksService.Delete(existing);
        }
    }
}