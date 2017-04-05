using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class BookDataToBookAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(BookDataToBookAutoMapperProfile);

        public BookDataToBookAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<BookData, Book>();

            // DAL => DB
            CreateMap<Book, BookData>();
            CreateMap<BookFilter, BookDataFilter>();
        }
    }
}