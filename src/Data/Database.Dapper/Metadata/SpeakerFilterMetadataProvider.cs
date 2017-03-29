using System;
using System.Collections.Generic;
using DapperExtensions;
using PrekenWeb.Data.Database.Dapper.Models;
using PrekenWeb.Data.DataModels;

namespace PrekenWeb.Data.Database.Dapper.Metadata
{
    internal class SpeakerFilterMetadataProvider : IFilterMetadataProvider
    {
        Type IFilterMetadataProvider.Type => typeof(SpeakerDataFilter);

        List<FilterMetadata> IFilterMetadataProvider.Metadata { get; } = new List<FilterMetadata>
        {
            new FilterMetadata<SpeakerDataFilter, SpeakerData>
            {
                FilterExpression = data => data.Id,
                FilterType = Operator.Eq,
                FilterValue = dataFilter => dataFilter.SpeakerId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<SpeakerDataFilter, SpeakerData>
            {
                FilterExpression = data => data.LastName,
                FilterType = Operator.Like,
                FilterValue = dataFilter => $"%{dataFilter.LastName}%",
                DefaultValue = default(string)
            }
        };
    }
}