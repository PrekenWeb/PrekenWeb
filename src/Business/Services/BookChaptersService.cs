using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Filters;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Models;

namespace Business.Services
{
    internal class BookChaptersService : Service<BookChapter, BookChapterFilter, BookChapterData, BookChapterDataFilter>, IBookChaptersService
    {
        public BookChaptersService(IMapper mapper, IBookChaptersGateway gateway)
            : base(mapper, gateway)
        {
        }
    }
}