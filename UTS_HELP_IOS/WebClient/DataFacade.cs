using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Linq;


namespace HELPiOS
{
	public enum Action { Get, Post, Put, Delete };

	public class DataFacade
	{
        private async Task<Response<T>> fetch<T>(string path, Object parameters, Object formData, Action action, JsonConverter converter)
		{
			DataSourceInterface db;
			// STUB
			// if (we have interwebs at request time)
			db = new RestClient();
			// else if (get, not post) AND (request is cache-compatible)
			//   db = new WorkshopCache();
			// else
			//   throw some no interwebs exception
            return await db.fetch<T>(path, parseParameters(parameters), parseFormData(formData), action, converter);
		}

		private string parseParameters(Object parameters)
		{
            if (parameters == null)
                return String.Empty;
			string serialisedParameters = JsonConvert.SerializeObject(parameters);
			Dictionary<string, string> parsedParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialisedParameters);
            return '?' + String.Join("&", parsedParameters.Select(x => x.Key + "=" + x.Value));
		}

		private HttpContent parseFormData(Object formData)
		{
			string serialisedFormData = JsonConvert.SerializeObject(formData);
            return new StringContent(serialisedFormData, Encoding.UTF8, "application/json");
		}

		public async Task<Response<T>> get<T>(string path, Object parameters)
		{
            return await fetch<T>(path, parameters, null, Action.Get, null);
        }

        public async Task<Response<T>> get<T>(string path, Object parameters, JsonConverter converter)
        {
            return await fetch<T>(path, parameters, null, Action.Get, converter);
		}

		public async Task<Response<T>> post<T>(string path, Object parameters, Object formData)
		{
            return await fetch<T>(path, parameters, formData, Action.Post, null);
		}

		public async Task<Response<T>> put<T>(string path, Object parameters, Object formData)
		{
            return await fetch<T>(path, parameters, formData, Action.Put, null);
		}

		public async Task<Response<T>> delete<T>(string path, Object parameters)
		{
            return await fetch<T>(path, parameters, null, Action.Delete, null);
		}

	}
}
