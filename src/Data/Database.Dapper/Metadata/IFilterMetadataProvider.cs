using System;
using System.Collections.Generic;
using PrekenWeb.Data.Gateways;

namespace PrekenWeb.Data.Database.Dapper.Metadata
{
    public interface IFilterMetadataProvider
    {
        Type Type { get; }
        List<FilterMetadata> Metadata { get; }
    }
}