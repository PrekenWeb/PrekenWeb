using System;
using System.Collections.Generic;

namespace DapperFilterExtensions.Filtering
{
    public interface IFilterMetadataProvider
    {
        Type Type { get; }
        IList<FilterMetadata> Metadata { get; }
    }
}