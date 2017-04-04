using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Gateways
{
    public interface ILanguagesGateway : IGateway<LanguageData, LanguageDataFilter>
    {
    }
}