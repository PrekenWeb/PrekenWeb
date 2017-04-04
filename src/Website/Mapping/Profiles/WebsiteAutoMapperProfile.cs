using AutoMapper;
using Data.Tables;
using Prekenweb.Website.Areas.Website.Models;

namespace Prekenweb.Website.Mapping.Profiles
{
    public class WebsiteAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(WebsiteAutoMapperProfile);

        public WebsiteAutoMapperProfile()
        {
            CreateMap<PreekZoeken, ZoekOpdracht>();
            CreateMap<PreekZoeken, ZoekOpdracht>();
            CreateMap<ZoekOpdracht, PreekZoeken>();
        }
    }
}