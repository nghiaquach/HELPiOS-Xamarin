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
		public string StudentId { get; set; }
		[JsonProperty(Required = Required.Always)]
		public DateTime DateOfBirth { get; set; }
		[JsonConverter(typeof(GenderConverter))]
		public Gender? Gender { get; set; }
		[JsonProperty(Required = Required.Always)]
		[JsonConverter(typeof(DegreeConverter))]
		public Degree Degree { get; set; }
		[JsonProperty(Required = Required.Always)]
		[JsonConverter(typeof(StringEnumConverter))]
		public Status Status { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string FirstLanguage { get; set; }
		public string Background { get; set; }
		[JsonConverter(typeof(DegreeDetailsConverter))]
		public DegreeDetails? DegreeDetails { get; set; }
		public string AltContact { get; set; }
		public string PreferredName { get; set; }
		public bool? HSC { get; set; }
		public string HSCMark { get; set; }
		public bool? IELTS { get; set; }
		public string IELTSMark { get; set; }
		public bool? TOEFL { get; set; }
		public string TOEFLMark { get; set; }
		public bool? TAFE { get; set; }
		public string TAFEMark { get; set; }
		public bool? InsearchDEEP { get; set; }
		public string InsearchDEEPMark { get; set; }
		public bool? InsearchDiploma { get; set; }
		public string InsearchDiplomaMark { get; set; }
		public bool? FoundationCourse { get; set; }
		public string FoundationCourseMark { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string CreatorId { get; set; }
		public DateTime? Created { get; set; }

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
			default:
				writer.WriteValue("5th");
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
			default: return DegreeDetails.FifthYr;
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
			default:
				writer.WriteValue('X');
				break;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch ((char)reader.Value)
			{
			case 'M': return Gender.Male;
			case 'F': return Gender.Female;
			default: return Gender.Other;
			}
		}
	}
}

