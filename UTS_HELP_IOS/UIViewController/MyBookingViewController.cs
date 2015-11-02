
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

			//handle logout button action
			myProfileButton.Clicked += (o, e) => {
				RegisterViewController registerViewController = (RegisterViewController)AppDelegate.Storyboard.InstantiateViewController ("RegisterViewController");
				this.PresentViewController(registerViewController, true, null);
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
			SessionBookingList sessionBookingList = new SessionBookingList ();

			Student student = AppParam.Instance.student;

			List<WorkshopBooking> wkBookingList = await workshopBookingList.getUpcomingByStudent (student);
			List<SessionBooking> ssBookingList = await sessionBookingList.getUpcomingByStudent (student);

			myBookingTableView.Source = new MyBookingTableSource (this,wkBookingList,ssBookingList);
			myBookingTableView.ReloadData ();
		}
	}
}

