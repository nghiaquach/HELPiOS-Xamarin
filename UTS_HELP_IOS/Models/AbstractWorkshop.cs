using System;

namespace HELPiOS
{
	abstract class AbstractWorkshop
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
		public bool? archived { get; set; }
	}
}

