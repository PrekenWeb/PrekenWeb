using DapperExtensions;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Metadata
{
    internal interface IPredicateFactory
    {
        IPredicate GetPredicate<TFilter, TData>(TFilter filter)
            where TFilter : DataFilter<TFilter, TData>
            where TData : class;
    }
}