using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace HELPiOS
{
	public class NewBookingTableSource : UITableViewSource
	{

		List<SingleWorkshop> singleWorkhopList = new List<SingleWorkshop> ();
		UIViewController newBookingViewController;
		NewBookingDetailViewController newBookingDetailViewController;

		NSString cellIdentifier = new NSString ("TableCell");

		public NewBookingTableSource (UIViewController newBookingViewController, List<SingleWorkshop> singleWorkhopList)
		{
			this.singleWorkhopList = singleWorkhopList;
			this.newBookingViewController = newBookingViewController;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return 5;
		}


		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return "Available Workshop Booking";
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			//default cell
			UITableViewCell dCell = new UITableViewCell (UITableViewCellStyle.Default, (NSString)"cell") {
				SelectionStyle = UITableViewCellSelectionStyle.None,
			};
			dCell.TextLabel.TextColor = UIColor.Red;
			dCell.UserInteractionEnabled = false;
			// request a recycled cell to save memory
			MyBookingTableCell cell = tableView.DequeueReusableCell (cellIdentifier) as MyBookingTableCell;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new MyBookingTableCell (cellIdentifier);
			}

			if (indexPath.Section == 0) {
				if (singleWorkhopList.Count == 0) {
					dCell.TextLabel.Text = "No Records";
					return dCell;
				} else {
					if (singleWorkhopList [indexPath.Row] != null) {
						cell.UpdateCell (singleWorkhopList [indexPath.Row].topic + ""
						, singleWorkhopList [indexPath.Row].StartDate + "");
					}
					
				}
				if (singleWorkhopList [indexPath.Row] != null) {
					cell.UpdateCell (singleWorkhopList [indexPath.Row].topic + ""
					, singleWorkhopList [indexPath.Row].EndDate + "");
				}


			}
			return cell;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			SingleWorkshop singleWorkshop = singleWorkhopList[indexPath.Row];

			newBookingDetailViewController = (NewBookingDetailViewController)AppDelegate.Storyboard.InstantiateViewController ("NewBookingDetailViewController");
			//New BookingDetailViewController
			if (singleWorkshop != null) {
				newBookingDetailViewController.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
				newBookingDetailViewController.singleWorkshop = singleWorkshop;
				newBookingViewController.PresentViewController (newBookingDetailViewController, true, null);
			}
			//deselect row
			tableView.DeselectRow (indexPath, true);
		}
	}
}

