using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Database.Dapper.Models;

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