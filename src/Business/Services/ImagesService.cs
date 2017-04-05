using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Database.Dapper.Models;

namespace Business.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IMapper _mapper;
        private readonly IImagesGateway _imagesGateway;

        public ImagesService(IMapper mapper, IImagesGateway imagesGateway)
        {
            _mapper = mapper;
            _imagesGateway = imagesGateway;
        }

        public async Task<IEnumerable<Image>> Get(ImageFilter filter)
        {
            var dataFilter = _mapper.Map<ImageFilter, ImageDataFilter>(filter);
            var imagesData = await _imagesGateway.Get(dataFilter);
            var images = _mapper.Map<IEnumerable<ImageData>, IEnumerable<Image>>(imagesData);
            return images;
        }

        public async Task<Image> GetSingle(int id)
        {
            var imageData = await _imagesGateway.GetSingle(id);
            var image = _mapper.Map<ImageData, Image>(imageData);
            return image;
        }

        public async Task<int> Add(Image image)
        {
            var imageData = _mapper.Map<Image, ImageData>(image);
            return await _imagesGateway.Add(imageData);
        }

        public async Task<bool> Update(Image image)
        {
            var imageData = _mapper.Map<Image, ImageData>(image);
            return await _imagesGateway.Update(imageData);
        }

        public async Task<bool> Delete(Image image)
        {
            var imageData = _mapper.Map<Image, ImageData>(image);
            return await _imagesGateway.Delete(imageData);
        }
    }
}