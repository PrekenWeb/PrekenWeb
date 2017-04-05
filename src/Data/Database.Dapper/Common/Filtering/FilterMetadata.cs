using System;
using System.Linq.Expressions;
using DapperExtensions;

namespace Data.Database.Dapper.Common.Filtering
{
    public class FilterMetadata
    {
    }

    public class FilterMetadata<TFilter, TData> : FilterMetadata where TFilter : DataFilter<TFilter, TData>
    {
        public Expression<Func<TData, object>> FilterExpression { get; set; }

        public Operator FilterType { get; set; }

        public Func<TFilter, object> FilterValue { get; set; }
        public object DefaultValue { get; set; }
    }
}