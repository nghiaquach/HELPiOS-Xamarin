
using System;

using Foundation;
using UIKit;

namespace HELPiOS
{
	public partial class HistoryBookingDetailViewController : UIViewController
	{
		public WorkshopBooking wkBooking { get; set;}
		public SessionBooking ssBooking { get; set;}

		public HistoryBookingDetailViewController (IntPtr handle) : base (handle)
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

			if (ssBooking == null) {
				updateButton.Hidden = false;
			}
			else
				updateButton.Hidden = true;

			//disable edit for text field
			descriptionTextView.Editable = false;

			//handle back button action
			backButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};

			updateButton.TouchUpInside += (o, e) => {
				//update session booking

			};
			//			Console.WriteLine ("Load My Bookings Detail View");
			this.showBookingDetail ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void showBookingDetail ()
		{		
			if (wkBooking != null) {
				updateButton.Hidden = true;
				descriptionTextView.Hidden = false;
				descriptionTextView.Text = wkBooking.description == null ? "No description" : wkBooking.description;
			} else {
				
				if (ssBooking.AppointmentsOther != null) {
					descriptionTextView.Text = ssBooking.AppointmentsOther;
				} else {
					descriptionTextView.Editable = true;
				}

			}

			historyBookingDetailTable.Source = new HistoryBookingDetailTableSource (wkBooking,ssBooking);
			historyBookingDetailTable.ReloadData ();
		}

		private void updateSessionBooking(){
			
		}
	}
}

