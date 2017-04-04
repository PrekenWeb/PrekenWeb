using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Gateways
{
    public interface ISpeakersGateway: IGateway<SpeakerData, SpeakerDataFilter>
    {
    }
}