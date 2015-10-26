using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace HELPiOS
{
	partial class HistoryViewController : UIViewController
	{
		public WorkshopBooking workshopBooking { get; set;}

		public HistoryViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidAppear (bool animate){
			// Perform any additional setup after loading the view, typically from a nib.
			this.showPastBookingList ();

			//handle logout button action
			logoutButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};

		}

		private async void showPastBookingList ()
		{
			LoadingOverlay.Instance.showLoading (this);

			WorkshopBookingList workshopBookingList = new WorkshopBookingList ();

			Student student = AppParam.Instance.student;

			List<WorkshopBooking> wkBookingList = await workshopBookingList.getPastByStudent (student);

			historyBookingTable.Source = new MyBookingTableSource (this,wkBookingList);
			historyBookingTable.ReloadData ();
		}


	}
}
