using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace HELPiOS
{
	partial class HistoryViewController : UIViewController
	{
		public HistoryViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidAppear (bool animate){
			// Perform any additional setup after loading the view, typically from a nib.
			this.showPastBookingList ();
		}

		private async void showPastBookingList ()
		{
			LoadingOverlay.Instance.showLoading (this);

			WorkshopBookingList workshopBookingList = new WorkshopBookingList ();

			Student std = new Student ();
			std.studentID = "11875360";

			List<WorkshopBooking> wkBookingList = await workshopBookingList.getPastByStudent (std);

			historyBookingTable.Source = new MyBookingTableSource (this,wkBookingList);
			historyBookingTable.ReloadData ();
		}


	}
}
