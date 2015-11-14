using System.Collections.Generic;
using System.Threading.Tasks;
using Prekenweb.Models.Dtos;

namespace App.Shared.Api
{
	public interface IPrekenwebApiWrapper
	{
		Task<IEnumerable<Preek>> NieuwePreken ();
	    Task<IEnumerable<Preek>> PreekZoeken(string zoekterm);
	}

}

