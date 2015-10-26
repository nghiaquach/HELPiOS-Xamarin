using System;

namespace HELPiOS
{
	public class WorkshopBooking
	{
		public int BookingId { get; set; }
		public int workshopID { get; set; }
		public string studentID { get; set; }
		public string topic { get; set; }
		public string description { get; set; }
		public string targetingGroup { get; set; }
		public int campusID { get; set; }
		public DateTime starting { get; set; }
		public DateTime ending { get; set; }
		public int maximum { get; set; }
        public int? cutoff { get; set; }
        public bool? canceled { get; set; }
        public bool? attended { get; set; }
        public int? WorkShopSetID { get; set; }
		public string type { get; set; }
        public int? reminder_num { get; set; }
		public int reminder_sent { get; set; }
        public string WorkshopArchived { get; set; }
        public bool? BookingArchived { get; set; }
	}
}

