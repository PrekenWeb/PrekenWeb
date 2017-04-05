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
    internal class ImagesRepository : IImagesRepository
    {
        private readonly IImagesService _imagesService;
        private readonly IMapper _mapper;

        public ImagesRepository(IImagesService imagesService, IMapper mapper)
        {
            _imagesService = imagesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageViewModel>> Get(ImageFilterModel filterModel)
        {
            var filter = _mapper.Map<ImageFilter>(filterModel);
            var images = await _imagesService.Get(filter);
            return _mapper.Map<IEnumerable<ImageViewModel>>(images);
        }

        public async Task<ImageViewModel> GetSingle(int id)
        {
            var image = await _imagesService.GetSingle(id);
            if (image == null) throw new ItemNotFoundException();
            return _mapper.Map<ImageViewModel>(image);
        }

        public async Task<int> Add(ImageViewModel imageModel)
        {
            var image = _mapper.Map<Image>(imageModel);
            return await _imagesService.Add(image);
        }

        public async Task<bool> Update(ImageViewModel imageModel)
        {
            var existing = _imagesService.GetSingle(imageModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var image = _mapper.Map<Image>(imageModel);
            return await _imagesService.Update(image);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _imagesService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _imagesService.Delete(existing);
        }
    }
}