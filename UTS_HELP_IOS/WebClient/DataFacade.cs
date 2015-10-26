using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace HELPiOS
{
	public enum Action { Get, Post, Put, Delete };

	public class DataFacade
	{
		private async Task<Response<T>> fetch<T>(string path, Object parameters, Object formData, Action action)
		{
			DataSourceInterface db;
			// STUB
			// if (we have interwebs at request time)
			db = new RestClient();
			// else if (get, not post) AND (request is cache-compatible)
			//   db = new WorkshopCache();
			// else
			//   throw some no interwebs exception
			return await db.fetch<T>(path, parseParameters(parameters), parseFormData(formData), action);
		}

		private string parseParameters(Object parameters)
		{
			string serialisedParameters = JsonConvert.SerializeObject(parameters);
			Dictionary<string, string> parsedParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialisedParameters);
			NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
			foreach (KeyValuePair<string, string> parameter in parsedParameters)
			{
				queryString[parameter.Key] = parameter.Value;
			}
			return '?' + queryString.ToString();
		}

		private HttpContent parseFormData(Object formData)
		{
			string serialisedFormData = JsonConvert.SerializeObject(formData);
			return new StringContent(serialisedFormData);
		}

		public async Task<Response<T>> get<T>(string path, Object parameters)
		{
			return await fetch<T>(path, parameters, null, Action.Get);
		}

		public async Task<Response<T>> post<T>(string path, Object parameters, Object formData)
		{
			return await fetch<T>(path, parameters, formData, Action.Post);
		}

		public async Task<Response<T>> put<T>(string path, Object parameters, Object formData)
		{
			return await fetch<T>(path, parameters, formData, Action.Put);
		}

		public async Task<Response<T>> delete<T>(string path, Object parameters)
		{
			return await fetch<T>(path, parameters, null, Action.Delete);
		}

	}
}
