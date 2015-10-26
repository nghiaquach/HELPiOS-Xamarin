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
	public class RestService : IRestService
	{
		HttpClient client;


		//public List<Lecturers> Items { get; private set; }
		public List<WorkshopBooking> Items { get; private set; }

		public RestService ()
		{
			if (client == null) {
				client = new HttpClient ();
				client.MaxResponseContentBufferSize = 256000;
				client.DefaultRequestHeaders.Add (Constants.AppKey, Constants.AppValue);
			}
		}

//		public async Task<List<Lecturers>> GetLecturerList ()
//		{
//			Items = new List<Lecturers> ();
//
//			var uri = new Uri (string.Format (Constants.ListLecturersUrl, string.Empty));
//
//			try {
//				var response = await client.GetAsync (uri);
//				if (response.IsSuccessStatusCode) {
//					var content = await response.Content.ReadAsStringAsync ();
//
//					Console.WriteLine("reponse message:" + content);
//
//					var results = Newtonsoft.Json.Linq.JObject.Parse(content).SelectToken("Results").ToString();
//					Items = JsonConvert.DeserializeObject <List<Lecturers>>(results);
//				}
//			} catch (Exception ex) {
//				Debug.WriteLine (@"ERROR {0}", ex.Message);
//			}
//
//			return Items;
//		}


		public async Task<List<WorkshopBooking>> GetWorkshopBookingList()
		{
			Items = new List<WorkshopBooking> ();

			var uri = new Uri (string.Format (Constants.workShopBookingUrl, string.Empty));

			try {
				var response = await client.GetAsync (uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();
					LoadingOverlay.Instance.Hide ();
					Console.WriteLine("reponse message:" + content);

					var results = Newtonsoft.Json.Linq.JObject.Parse(content).SelectToken("Results").ToString();
					Items = JsonConvert.DeserializeObject <List<WorkshopBooking>>(results);
				}
			} catch (Exception ex) {
				Debug.WriteLine (@"ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task<bool> doLogin(Login login)
		{

			var uri = new Uri (string.Format (Constants.loginUrl, string.Empty));
			//HttpContent reqContent = this.parseFormData (login);
		//	HttpContent reqContent = new HttpContent();

			var jsonRequest = JsonConvert.SerializeObject(login); 
			var httpContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
		

			try {
				var response = await client.PostAsync(uri,httpContent);
				LoadingOverlay.Instance.Hide ();
				Console.WriteLine("IsSuccessStatusCode message:" + response.IsSuccessStatusCode);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();

					Console.WriteLine("reponse message:" + content);

					var results = Newtonsoft.Json.Linq.JObject.Parse(content).SelectToken("Results").ToString();

					if(results.Equals("1")){
						return true;
					}
					return false;
				}
			} catch (Exception ex) {
				Debug.WriteLine (@"ERROR {0}", ex.Message);
			}

			return false;
		}

		public HttpContent parseFormData(Object formData)
		{
			string serialisedFormData = JsonConvert.SerializeObject(formData);
			return new StringContent(serialisedFormData);
		}
				
	}


}
