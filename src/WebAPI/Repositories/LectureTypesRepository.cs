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
    internal class LectureTypesRepository : ILectureTypesRepository
    {
        private readonly ILectureTypesService _lectureTypesService;
        private readonly IMapper _mapper;

        public LectureTypesRepository(ILectureTypesService lectureTypesService, IMapper mapper)
        {
            _lectureTypesService = lectureTypesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LectureTypeViewModel>> Get(LectureTypeFilterModel filterModel)
        {
            var filter = _mapper.Map<LectureTypeFilter>(filterModel);
            var lectureTypes = await _lectureTypesService.Get(filter);
            return _mapper.Map<IEnumerable<LectureTypeViewModel>>(lectureTypes);
        }

        public async Task<LectureTypeViewModel> GetSingle(int id)
        {
            var lectureType = await _lectureTypesService.GetSingle(id);
            if (lectureType == null) throw new ItemNotFoundException();
            return _mapper.Map<LectureTypeViewModel>(lectureType);
        }

        public async Task<int> Add(LectureTypeViewModel lectureTypeModel)
        {
            var lectureType = _mapper.Map<LectureType>(lectureTypeModel);
            return await _lectureTypesService.Add(lectureType);
        }

        public async Task<bool> Update(LectureTypeViewModel lectureTypeModel)
        {
            var existing = _lectureTypesService.GetSingle(lectureTypeModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var lectureType = _mapper.Map<LectureType>(lectureTypeModel);
            return await _lectureTypesService.Update(lectureType);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _lectureTypesService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _lectureTypesService.Delete(existing);
        }
    }
}