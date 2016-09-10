using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prekenweb.Models.Dtos;

namespace App.Shared.Api
{
    public class PrekenwebApiWrapper : IPrekenwebApiWrapper
    { 
		private readonly string _rootUri;
		private readonly HttpClient _httpClient;

		public PrekenwebApiWrapper() : this ("http://test.api.prekenweb.nl/api/")
		{
		}

		public PrekenwebApiWrapper(string rooturi)
        { 
			_rootUri = rooturi;

			var handler = new HttpClientHandler();
			_httpClient = new HttpClient(handler);
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");  }

        public async Task<IEnumerable<Preek>> NieuwePreken()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _rootUri + "Preek/NieuwePreken");
            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Preek>>(responseString);
        }


        public async Task<IEnumerable<Preek>> PreekZoeken(string zoekterm)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _rootUri + "Preek/NieuwePreken");
            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Preek>>(responseString);
            result = result.Where(x => x.PreekTitel.Contains(zoekterm)); //todo: moet later natuurlijk serverside
            return result;
        }
    }
}

