
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
	public partial class MyBookingDetailViewController : UIViewController
	{
		public WorkshopBooking workshopBooking { get; set;}
		
		public MyBookingDetailViewController (IntPtr handle) : base (handle)
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

			//handle cancel button action
			cancelButton.TouchUpInside += (o, e) => {

				this.cancelWorkshopBooking();
//				this.cancelWorkshopWaiting();

				LoadingOverlay.Instance.showLoading(this);
			};

//			Console.WriteLine ("Load My Bookings Detail View");
			this.showBookingDetail ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void showBookingDetail ()
		{			
			descriptionTextView.Text = workshopBooking.description==null?"No description":workshopBooking.description;
			myBookingDetailTable.Source = new MyBookingDetailTableSource (workshopBooking);
			myBookingDetailTable.ReloadData ();
		}



		private async void cancelWorkshopBooking(){
			WorkshopBookingList workshopBookingList = new WorkshopBookingList();
			try{
//				await workshopBookingList.cancelBooking(workshopBooking,AppParam.Instance.student);
			}
			catch(Exception ex){
				AppParam.Instance.showAlertMessage ("Workshop Booking", "Cancel workshop booking Fail!");
			}

		}

		private async void cancelWorkshopWaiting(){
			WorkshopBookingList workshopBookingList = new WorkshopBookingList();
			try{
				
//				workshopBookingList.cancelWaiting(WebKit,student);
			}
			catch(Exception ex){
				AppParam.Instance.showAlertMessage ("Workshop Waiting", "Cancel workshop waiting Fail!");
			}

		}
	}
}

