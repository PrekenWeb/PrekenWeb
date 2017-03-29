using DapperExtensions;
using PrekenWeb.Data.Database.Dapper.Models;

namespace PrekenWeb.Data.Database.Dapper.Metadata
{
    internal interface IPredicateFactory
    {
        IPredicate GetPredicate<TFilter, TData>(TFilter filter)
            where TFilter : DataFilter<TFilter, TData>
            where TData : class;
    }
}