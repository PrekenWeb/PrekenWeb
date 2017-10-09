using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Interfaces.Gateways
{
    public interface ILanguagesGateway : IGateway<LanguageData, LanguageDataFilter>
    {
    }
}