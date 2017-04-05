using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class ImageDataToImageAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(ImageDataToImageAutoMapperProfile);

        public ImageDataToImageAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<ImageData, Image>();

            // DAL => DB
            CreateMap<Image, ImageData>();
            CreateMap<ImageFilter, ImageDataFilter>();
        }
    }
}