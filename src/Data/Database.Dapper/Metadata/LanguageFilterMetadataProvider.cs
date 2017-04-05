using System;
using System.Collections.Generic;
using DapperExtensions;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class LanguageFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(LanguageDataFilter);

        List<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
        {
            new FilterMetadata<LanguageDataFilter, LanguageData>
            {
                FilterExpression = data => data.Id,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.LanguageId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<LanguageDataFilter, LanguageData>
            {
                FilterExpression = data => data.Code,
                FilterType = Operator.Like,
                FilterValue = filter => $"{filter.Code}",
                DefaultValue = default(string)
            }
        };
    }
}