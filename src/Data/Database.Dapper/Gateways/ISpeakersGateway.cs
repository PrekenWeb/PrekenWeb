using PrekenWeb.Data.Database.Dapper.Models;
using PrekenWeb.Data.DataModels;

namespace PrekenWeb.Data.Gateways
{
    public interface ISpeakersGateway: IGateway<SpeakerData, SpeakerDataFilter>
    {
    }
}