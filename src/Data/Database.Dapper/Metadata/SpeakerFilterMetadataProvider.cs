using System;
using System.Collections.Generic;
using DapperExtensions;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Metadata
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
                FilterValue = filter => filter.SpeakerId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<SpeakerDataFilter, SpeakerData>
            {
                FilterExpression = data => data.LanguageId,
                FilterType = Operator.Eq,
                FilterValue = filter => filter.LanguageId,
                DefaultValue = default(int?)
            },
            new FilterMetadata<SpeakerDataFilter, SpeakerData>
            {
                FilterExpression = data => data.LastName,
                FilterType = Operator.Like,
                FilterValue = filter => $"%{filter.LastName}%",
                DefaultValue = default(string)
            }
        };
    }
}