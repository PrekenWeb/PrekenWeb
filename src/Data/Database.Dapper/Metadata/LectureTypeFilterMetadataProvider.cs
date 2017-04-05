using System;
using System.Collections.Generic;
using DapperExtensions;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class LectureTypeFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(LectureTypeDataFilter);

        List<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
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