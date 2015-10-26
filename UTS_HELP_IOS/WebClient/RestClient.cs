using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HELPiOS
{
	public class RestClient : DataSourceInterface
	{
		private const int BufferSize = 256000;
		private const String AppKey = "AppKey";
		private const String AppValue = "123456";
		private const String BaseUri = "http://g11.cloudapp.net/api/";

		private HttpClient client;

		public RestClient()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = BufferSize;
			client.DefaultRequestHeaders.Add(AppKey, AppValue);
		}

		public async Task<string> fetchString(string path, string parameters, HttpContent formData, Action action)
		{
			string fullUri = BaseUri + '/' + path;
			Console.WriteLine(@"msg request {0}", fullUri + parameters);
			try
			{
				HttpResponseMessage response;
				switch (action)
				{
				case Action.Post:
					response = await client.PostAsync(fullUri + parameters, formData);
					break;
				case Action.Put:
					response = await client.PutAsync(fullUri + parameters, formData);
					break;
				case Action.Delete:
					response = await client.DeleteAsync(fullUri + parameters);
					break;
				default:
					response = await client.GetAsync(fullUri + parameters);
					break;
				}
				LoadingOverlay.Instance.hideLoading();

				if (response.IsSuccessStatusCode)
				{
					
					Console.WriteLine(@"msg reponse {0}", response.Content.ReadAsStringAsync());
					return await response.Content.ReadAsStringAsync();
				}
				else
				{
					throw new Exception(await response.Content.ReadAsStringAsync());
				}
			}
			catch (Exception e)
			{
				LoadingOverlay.Instance.hideLoading();
				Console.WriteLine(@"ERROR {0}", e.Message);
				throw e;
			}
		}

        public async Task<Response<T>> fetch<T>(string path, string parameters, HttpContent formData, Action action, JsonConverter converter)
		{
            string respBody = await fetchString(path, parameters, formData, action);
            if (converter == null)
                return JsonConvert.DeserializeObject<Response<T>>(respBody);
            else
                return JsonConvert.DeserializeObject<Response<T>>(respBody, new JsonSerializerSettings() { Converters = { converter } });
		}

	}
}
