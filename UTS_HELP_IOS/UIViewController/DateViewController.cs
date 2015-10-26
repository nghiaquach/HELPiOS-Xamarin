
using System;

using Foundation;
using UIKit;

namespace HELPiOS
{
	public partial class DateViewController : UIViewController
	{
		public NSDate selectedDate;
		public DateViewController () : base ("DateViewController", null)
		{
		}

		public DateViewController (IntPtr handle): base (handle)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			//handle back button action
			leftBarButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};
			//handle back button action
			rightBarButton.Clicked += (o, e) => {
				selectedDate = datePicker.Date;
				Console.WriteLine(datePicker.Date);
				this.DismissViewControllerAsync(true);
			};
		}
	}
}

