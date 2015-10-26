
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
	public partial class MyBookingViewController : UIViewController
	{
		public static HelpItemManager HelpManager { get; private set; }

		public MyBookingViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Console.WriteLine ("Load My Bookings View");

			//handle logout button action
			logoutButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};

		}


		public override void ViewDidAppear (bool animate){
			// Perform any additional setup after loading the view, typically from a nib.
			this.showUpcomingBookingList ();
		}



		private async void showUpcomingBookingList ()
		{
			LoadingOverlay.Instance.showLoading (this);

			WorkshopBookingList workshopBookingList = new WorkshopBookingList ();

			Student std = new Student ();
			std.studentID = "11875360";

			List<WorkshopBooking> wkBookingList = await workshopBookingList.getUpcomingByStudent (std);

			myBookingTableView.Source = new MyBookingTableSource (this,wkBookingList);
			myBookingTableView.ReloadData ();
		}
	}
}

