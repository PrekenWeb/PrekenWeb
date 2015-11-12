using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prekenweb.Models.Dtos;

namespace App.Shared
{
	public interface IPrekenwebApiWrapper
	{
		Task<IEnumerable<Preek>> NieuwePreken ();
	}

}

