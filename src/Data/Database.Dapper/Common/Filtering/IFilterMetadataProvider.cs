using System;
using System.Collections.Generic;

namespace Data.Database.Dapper.Common.Filtering
{
    public interface IFilterMetadataProvider
    {
        Type Type { get; }
        List<FilterMetadata> Metadata { get; }
    }
}