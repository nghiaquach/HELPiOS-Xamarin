
using System;

using Foundation;
using UIKit;

namespace HELPiOS
{
	public partial class NewBookingViewController : UIViewController
	{

		enum SearchSelection
		{
			Topic,
			Date,
			Location,
			Tutor
		};

		private SearchSelection selectedItem;

		public NewBookingViewController () : base ("NewBookingViewController", null)
		{
		}

		public NewBookingViewController (IntPtr handle) : base (handle)
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
			segmentControl.ValueChanged += (sender, e) => {
			var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
				switch (selectedSegmentId){
					
					case 0:
						selectedItem = SearchSelection.Topic;
						break;
					case 1:
						selectedItem = SearchSelection.Date;
					//this.NavigationController.PushViewController (pinkViewController, true);
					DateViewController dateVC = (DateViewController)AppDelegate.Storyboard.InstantiateViewController ("DateViewController");

					//AppDelegate.mainTabbarController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
					this.PresentViewController(dateVC, true, null);
						break;
					case 2:
						selectedItem = SearchSelection.Location;
						break;
					case 3:
						selectedItem = SearchSelection.Tutor;
						break;
				}
			};
		}
			
	}
}

