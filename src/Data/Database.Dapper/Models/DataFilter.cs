namespace Data.Database.Dapper.Models
{
    public class DataFilter<TFilter, TData> where TFilter : DataFilter<TFilter, TData>
    {
        //internal List<FilterMetadata<TFilter, TData>> Metadata { get; } = new List<FilterMetadata<TFilter, TData>>();
    }
}