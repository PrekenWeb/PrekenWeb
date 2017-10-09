using DapperExtensions;

namespace DapperFilterExtensions.Filtering
{
    public interface IPredicateFactory
    {
        IPredicate GetPredicate<TFilter, TData>(TFilter filter)
            where TFilter : DataFilter<TFilter, TData>
            where TData : class;
    }
}