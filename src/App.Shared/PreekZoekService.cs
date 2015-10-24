using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prekenweb.Models.Dtos;

namespace App.Shared
{
    public class TestClass
    {
        public TestClass()
        {
        }

        public async Task<IEnumerable<Preek>> NieuwePreken()
        { 
            var handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");


            //http://10.211.55.5
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://10.211.55.5/PrekenWebApi/api/Preek/NieuwePreken");
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.test.prekenweb.nl/api/Preek/NieuwePreken");
			var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Preek>>(responseString); 
        }
    }

}

