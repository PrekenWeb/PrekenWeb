using System;
using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Filters;
using Data.Models;

namespace Business.Mapping.Profiles
{
    public class DataModelToBusinessModelAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(DataModelToBusinessModelAutoMapperProfile);

        public DataModelToBusinessModelAutoMapperProfile()
        {
            CreateTwoWayMap<BookData, BookDataFilter, Book, BookFilter>();
            CreateTwoWayMap<ImageData, ImageDataFilter, Image, ImageFilter>();
            CreateTwoWayMap<LanguageData, LanguageDataFilter, Language, LanguageFilter>();
            CreateTwoWayMap<LectureData, ViewLectureData, LectureDataFilter, Lecture, ViewLecture, LectureFilter>();
            CreateTwoWayMap<LectureTypeData, LectureTypeDataFilter, LectureType, LectureTypeFilter>();
            CreateTwoWayMap<SpeakerData, SpeakerDataFilter, Speaker, SpeakerFilter>();
        }

        // Private methods
        #region CreateTwoWayMap

        private void CreateTwoWayMap<TData, TDataFilter, TBusiness, TBusinessFilter>(
            Action<IMappingExpression<TData, TBusiness>> afterCreateDataToBusinessMapAction = null,
            Action<IMappingExpression<TBusiness, TData>> afterCreateBusinessToDataMapAction = null,
            Action<IMappingExpression<TBusinessFilter, TDataFilter>> afterCreateBusinessFilterToDataFilterMapAction = null
            )
        {
            // DB => DAL
            var dataToBusinessExpression = CreateMap<TData, TBusiness>();
            afterCreateDataToBusinessMapAction?.Invoke(dataToBusinessExpression);

            // DAL => DB
            var businessToDataExpression = CreateMap<TBusiness, TData>();
            afterCreateBusinessToDataMapAction?.Invoke(businessToDataExpression);

            var businessFilterToDataFilterExpression = CreateMap<TBusinessFilter, TDataFilter>();
            afterCreateBusinessFilterToDataFilterMapAction?.Invoke(businessFilterToDataFilterExpression);
        }

        private void CreateTwoWayMap<TData, TViewData, TDataFilter, TBusiness, TViewBusiness, TBusinessFilter>(
            Action<IMappingExpression<TData, TBusiness>> afterCreateDataToBusinessMapAction = null,
            Action<IMappingExpression<TViewData, TViewBusiness>> afterCreateViewDataToViewBusinessMapAction = null,
            Action<IMappingExpression<TBusiness, TData>> afterCreateBusinessToDataMapAction = null,
            Action<IMappingExpression<TViewBusiness, TData>> afterCreateViewBusinessToDataMapAction = null,
            Action<IMappingExpression<TBusinessFilter, TDataFilter>> afterCreateBusinessFilterToDataFilterMapAction = null
            )
        {
            // DB => DAL
            var dataToBusinessExpression = CreateMap<TData, TBusiness>();
            afterCreateDataToBusinessMapAction?.Invoke(dataToBusinessExpression);

            var viewDataToViewBusinessExpression = CreateMap<TViewData, TViewBusiness>();
            afterCreateViewDataToViewBusinessMapAction?.Invoke(viewDataToViewBusinessExpression);

            // DAL => DB
            var businessToDataExpression = CreateMap<TBusiness, TData>();
            afterCreateBusinessToDataMapAction?.Invoke(businessToDataExpression);

            var viewBusinessToDataExpression = CreateMap<TViewBusiness, TData>();
            afterCreateViewBusinessToDataMapAction?.Invoke(viewBusinessToDataExpression);

            var businessFilterToDataFilterExpression = CreateMap<TBusinessFilter, TDataFilter>();
            afterCreateBusinessFilterToDataFilterMapAction?.Invoke(businessFilterToDataFilterExpression);
        }

        #endregion
    }
}