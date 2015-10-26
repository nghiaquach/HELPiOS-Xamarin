// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HELPiOS
{
	[Register ("NewBookingViewController")]
	partial class NewBookingViewController
	{
		[Outlet ("searchBar")]
		UIKit.UISearchBar searchBar { get; set; }

		[Outlet ("searchButton")]
		UIKit.UIBarButtonItem searchButton { get; set; }

		[Outlet ("segmentControl")]
		UIKit.UISegmentedControl segmentControl { get; set; }

		[Outlet ("resultTableView")]
		UIKit.UITableView resultTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISearchDisplayController searchDisplayController { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (searchDisplayController != null) {
				searchDisplayController.Dispose ();
				searchDisplayController = null;
			}
		}
	}
}
