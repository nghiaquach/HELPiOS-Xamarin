using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HELPiOS
{
	public abstract class AbstractWorkshop
	{
		public int? WorkshopId { get; set; }
		public string topic { get; set; }
		public string description { get; set; }
		public string targetingGroup { get; set; }
		public int maximum { get; set; }
		public int? WorkShopSetID { get; set; }
		public int? cutoff { get; set; }
		public string type { get; set; }
		public int BookingCount { get; set; }
		public bool? WorkshopArchived { get; set; }
	}

	public class AbstractWorkshopConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(AbstractWorkshop));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jo = JObject.Load(reader);
			string workshopType = jo["type"].Value<string>();
			switch (workshopType)
			{
			case "single":
				return jo.ToObject<SingleWorkshop>(serializer);
			case "multiple":
				return jo.ToObject<Programme>(serializer);
			default:
				throw new UnknownWorkshopType("Unknown workshop type: " + workshopType);
			}
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}