using System;
using System.Collections.Generic;
using DapperExtensions;
using DapperFilterExtensions.Filtering;
using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Metadata
{
    internal class ImageFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(ImageDataFilter);

        IList<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
        {
            new FilterMetadata<ImageDataFilter, ImageData>
            {
                FilterExpression = data => data.Id,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.ImageId,
                DefaultValue = default(int?)
            }
        };
    }
}