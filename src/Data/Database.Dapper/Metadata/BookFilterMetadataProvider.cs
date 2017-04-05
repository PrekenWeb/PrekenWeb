using System;
using System.Collections.Generic;
using DapperExtensions;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class BookFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(BookDataFilter);

        List<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
        {
            new FilterMetadata<BookDataFilter, BookData>
            {
                FilterExpression = data => data.Id,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.BookId,
                DefaultValue = default(int?)
            }
        };
    }
}