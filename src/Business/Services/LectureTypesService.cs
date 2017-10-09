using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Filters;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Models;

namespace Business.Services
{
    public class LectureTypesService : ILectureTypesService
    {
        private readonly IMapper _mapper;
        private readonly ILectureTypesGateway _lectureTypesGateway;

        public LectureTypesService(IMapper mapper, ILectureTypesGateway lectureTypesGateway)
        {
            _mapper = mapper;
            _lectureTypesGateway = lectureTypesGateway;
        }

        public async Task<IEnumerable<LectureType>> Get(LectureTypeFilter filter)
        {
            var dataFilter = _mapper.Map<LectureTypeFilter, LectureTypeDataFilter>(filter);
            var lectureTypesData = await _lectureTypesGateway.Get(dataFilter);
            var lectureTypes = _mapper.Map<IEnumerable<LectureTypeData>, IEnumerable<LectureType>>(lectureTypesData);
            return lectureTypes;
        }

        public async Task<LectureType> GetSingle(int id)
        {
            var lectureTypeData = await _lectureTypesGateway.GetSingle(id);
            var lectureType = _mapper.Map<LectureTypeData, LectureType>(lectureTypeData);
            return lectureType;
        }

        public async Task<int> Add(LectureType lectureType)
        {
            var lectureTypeData = _mapper.Map<LectureType, LectureTypeData>(lectureType);
            return await _lectureTypesGateway.Add(lectureTypeData);
        }

        public async Task<bool> Update(LectureType lectureType)
        {
            var lectureTypeData = _mapper.Map<LectureType, LectureTypeData>(lectureType);
            return await _lectureTypesGateway.Update(lectureTypeData);
        }

        public async Task<bool> Delete(LectureType lectureType)
        {
            var lectureTypeData = _mapper.Map<LectureType, LectureTypeData>(lectureType);
            return await _lectureTypesGateway.Delete(lectureTypeData);
        }
    }
}