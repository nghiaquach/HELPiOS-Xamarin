using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HELPiOS
{
	public enum Degree { UnderGrad, PostGrad };
	public enum DegreeDetails { FirstYr, SecondYr, ThirdYr, FourthYr, FifthYr };
	public enum Gender { Male, Female, Other };
	public enum Status { International, Permanent };

	public class Student
	{
		[JsonProperty(Required = Required.Always)]
        public string studentID { get; set; }
        public DateTime? dob { get; set; }
		[JsonConverter(typeof(GenderConverter))]
        public Gender? gender { get; set; }
		[JsonProperty(Required = Required.Always)]
		[JsonConverter(typeof(DegreeConverter))]
        public Degree degree { get; set; }
		[JsonProperty(Required = Required.Always)]
		[JsonConverter(typeof(StringEnumConverter))]
        public Status status { get; set; }
		[JsonProperty(Required = Required.Always)]
        public string first_language { get; set; }
		public string country_origin { get; set; }
        public string background { get; set; }
		[JsonConverter(typeof(DegreeDetailsConverter))]
        public DegreeDetails? degree_details { get; set; }
        public string alternative_contact { get; set; }
        public string preferred_name { get; set; }
		public bool? HSC { get; set; }
        public string HSC_mark { get; set; }
		public bool? IELTS { get; set; }
        public string IELTS_mark { get; set; }
		public bool? TOEFL { get; set; }
        public string TOEFL_mark { get; set; }
		public bool? TAFE { get; set; }
        public string TAFE_mark { get; set; }
		public bool? InsearchDEEP { get; set; }
        public string InsearchDEEP_mark { get; set; }
		public bool? InsearchDiploma { get; set; }
        public string InsearchDiploma_mark { get; set; }
        public bool? foundationcourse { get; set; }
        public string foundationcourse_mark { get; set; }
		[JsonProperty(Required = Required.Always)]
        public string creatorId { get; set; }
        public DateTime? created { get; set; }

		public Student()
		{
		}
	}

	public class DegreeConverter : StringEnumConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			switch ((Degree)value)
			{
			case Degree.PostGrad:
				writer.WriteValue("PG");
				break;
			default:
				writer.WriteValue("UG");
				break;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch ((string)reader.Value)
			{
			case "PG": return Degree.PostGrad;
			default: return Degree.UnderGrad;
			}
		}
	}

	public class DegreeDetailsConverter : StringEnumConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			switch ((DegreeDetails)value)
			{
			case DegreeDetails.FirstYr:
				writer.WriteValue("1st");
				break;
			case DegreeDetails.SecondYr:
				writer.WriteValue("2nd");
				break;
			case DegreeDetails.ThirdYr:
				writer.WriteValue("3rd");
				break;
			case DegreeDetails.FourthYr:
				writer.WriteValue("4th");
				break;
                case DegreeDetails.FifthYr:
                    writer.WriteValue("5th");
                    break;
			default:
                    writer.WriteNull();
				break;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch ((string)reader.Value)
			{
			case "1st": return DegreeDetails.FirstYr;
			case "2nd": return DegreeDetails.SecondYr;
			case "3rd": return DegreeDetails.ThirdYr;
			case "4th": return DegreeDetails.FourthYr;
                case "5th": return DegreeDetails.FifthYr;
                default: return null;
			}
		}
	}

	public class GenderConverter : StringEnumConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			switch ((Gender)value)
			{
			case Gender.Male:
				writer.WriteValue('M');
				break;
			case Gender.Female:
				writer.WriteValue('F');
				break;
                case Gender.Other:
                    writer.WriteValue('X');
                    break;
			default:
                    writer.WriteNull();
				break;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
            switch ((string)reader.Value)
			{
                case "M": return Gender.Male;
                case "F": return Gender.Female;
                case "X": return Gender.Other;
                default: return null;
			}
		}
	}
}

