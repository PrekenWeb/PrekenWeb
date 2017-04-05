using DapperExtensions;

namespace Data.Database.Dapper.Common.Filtering
{
    internal interface IPredicateFactory
    {
        IPredicate GetPredicate<TFilter, TData>(TFilter filter)
            where TFilter : DataFilter<TFilter, TData>
            where TData : class;
    }
}