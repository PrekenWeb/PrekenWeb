using DapperFilterExtensions.Filtering;
using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public class LanguageDataFilter : DataFilter<LanguageDataFilter, LanguageData>
    {
        public int? LanguageId { get; set; }
        public string Code { get; set; }
    }
}