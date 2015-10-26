
using System;

using Foundation;
using UIKit;
using CoreGraphics;

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

			Console.WriteLine("Load My Bookings View");
			//myBookingTableView = new UITableView(View.Bounds); // defaults to Plain style
//			myBookingTableView.Frame = new CGRect (100, 18, 100, 20);
			string[] tableItems = new string[] {"Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers"};
			myBookingTableView.Source = new MyBookingTableSource(tableItems);
			Add (myBookingTableView);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

