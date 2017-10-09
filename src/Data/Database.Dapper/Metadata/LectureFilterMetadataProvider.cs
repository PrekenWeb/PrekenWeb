using System;
using System.Collections.Generic;
using DapperExtensions;
using DapperFilterExtensions.Filtering;
using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class LectureFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(LectureDataFilter);

        IList<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
        {
            new FilterMetadata<LectureDataFilter, LectureData>
            {
                FilterExpression = data => data.Id,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.LectureId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<LectureDataFilter, LectureData>
            {
                FilterExpression = data => data.SpeakerId,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.SpeakerId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<LectureDataFilter, LectureData>
            {
                FilterExpression = data => data.CongregationId,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.CongregationId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<LectureDataFilter, LectureData>
            {
                FilterExpression = data => data.Title,
                FilterType = Operator.Like,
                FilterValue = dataFilter => $"%{dataFilter.Title}%",
                DefaultValue = default(string)
            }
        };
    }
}