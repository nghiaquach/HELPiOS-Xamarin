using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace HELPiOS
{
	public class WorkshopCache : DataSourceInterface
	{

        public Task<Response<T>> fetch<T>(string path, string parameters, HttpContent formData, Action action, JsonConverter converter)
		{
			return Task.FromResult(new Response<T>());
		}

	}
}
