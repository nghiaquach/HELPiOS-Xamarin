
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
	public partial class NewBookingDetailViewController : UIViewController
	{
		public SingleWorkshop singleWorkshop { get; set;}
		public SessionBooking sessionBooking { get; set;}
		
		public NewBookingDetailViewController (IntPtr handle) : base (handle)
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
			//disable edit for text field
			descriptionTextView.Editable = false;

			//handle back button action
			backButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};
			//book button
			bookButton.TouchUpInside += (o, e) => {
				this.bookWorkshop();
			};

//			Console.WriteLine ("Load My Bookings Detail View");
			this.showBookingDetail ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void showBookingDetail ()
		{	
			if (singleWorkshop != null) {
				descriptionTextView.Text = singleWorkshop.description == null ? "No description" : singleWorkshop.description;
				newBookingDetailTable.Source = new NewBookingDetailTableSource (singleWorkshop);

			}
			if (sessionBooking != null) {
				descriptionTextView.Text = sessionBooking.LearningIssues == null ? "No description" : sessionBooking.LearningIssues;
				newBookingDetailTable.Source = new NewBookingDetailTableSource (sessionBooking);
			}
			newBookingDetailTable.ReloadData ();

		}

		private async void bookWorkshop(){
			LoadingOverlay.Instance.showLoading(this);
			WorkshopBookingList workshopBookingList = new WorkshopBookingList();
			try{
				await workshopBookingList.createBooking(singleWorkshop,AppParam.Instance.student);
				await this.DismissViewControllerAsync(true);
			}
			catch(Exception ex){
				AppParam.Instance.showAlertMessage ("Workshop Booking", "Booking Fail!");
			}

		}
	}
}

