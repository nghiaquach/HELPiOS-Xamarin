using System;

namespace HELPiOS
{
	public class SingleWorkshop : AbstractWorkshop
	{
		public string campus { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int? reminder_num { get; set; }
		public int reminder_sent { get; set; }
	}
}

