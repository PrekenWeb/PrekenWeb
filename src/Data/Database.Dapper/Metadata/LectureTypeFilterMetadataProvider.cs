using System;
using System.Collections.Generic;
using DapperExtensions;
using DapperFilterExtensions.Filtering;
using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class LectureTypeFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(LectureTypeDataFilter);

        IList<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
        {
            new FilterMetadata<LectureTypeDataFilter, LectureTypeData>
            {
                FilterExpression = data => data.Id,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.LectureTypeId,
                DefaultValue = default(int?)
            }
        };
    }
}