
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

			//disable cancel button for the pass booking
			if(this.isBeforeNow(workshopBooking.starting)){
				cancelButton.Hidden = true;
			}
			//disable edit for text field
			descriptionTextView.Editable = false;

			//handle back button action
			backButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};
//			Console.WriteLine ("Load My Bookings Detail View");
			this.showBookingDetail ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void showBookingDetail ()
		{
			
			descriptionTextView.Text = workshopBooking.description==null?"No description":workshopBooking.description;

//			HelpManager = new HelpItemManager (new RestService ());
//			Task<List<WorkshopBooking>> workshopBookingTask = HelpManager.GetWorkshopBookingTasksAsync ();
//			List<WorkshopBooking> workshopBookingList = await workshopBookingTask;
//
			myBookingDetailTable.Source = new MyBookingDetailTableSource (workshopBooking);
			myBookingDetailTable.ReloadData ();
		}

		//Compare the date with current
		private bool isBeforeNow(DateTime date){
			DateTime dt2 = DateTime.Now;

			if(date.Date > dt2.Date)
			{
				//It's a later date
				return false;
			}
			else
			{
				//It's an earlier or equal date
				return true;
			}

		}
			
	}
}

