using System;
using System.Collections.Generic;

namespace Data.Database.Dapper.Metadata
{
    public interface IFilterMetadataProvider
    {
        Type Type { get; }
        List<FilterMetadata> Metadata { get; }
    }
}