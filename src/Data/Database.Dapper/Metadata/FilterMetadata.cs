using System;
using System.Linq.Expressions;
using DapperExtensions;
using PrekenWeb.Data.Database.Dapper.Models;

namespace PrekenWeb.Data.Database.Dapper.Metadata
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