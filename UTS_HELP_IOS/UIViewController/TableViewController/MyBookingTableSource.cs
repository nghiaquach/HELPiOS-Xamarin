using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace HELPiOS
{
	public class MyBookingTableSource : UITableViewSource {

		List<WorkshopBooking> workshopBookingList = new List<WorkshopBooking>();

		//		List<WorkshopBooking> bookingWorkShopList = new List<WorkshopBooking>();
		//		List<WorkshopBooking> bookedWorkhopList = new List<WorkshopBooking>();

		MyBookingDetailViewController myBookingDetailViewController;
		UIViewController myBookingViewController;

		NSString cellIdentifier = new NSString("TableCell");

		public MyBookingTableSource (UIViewController myBookingViewController,List<WorkshopBooking> workshopBookingList)
		{
			this.workshopBookingList = workshopBookingList;
			this.myBookingViewController = myBookingViewController;
			//			this.divideWorkshopList ();
		}
		//		//seperate booking and booked workshop 
		//		private void divideWorkshopList(){
		//			for (int i = 0; i < workshopBookingList.Count; i++) {
		//				WorkshopBooking wk = workshopBookingList [i];
		//
		//				DateTime startingDate = wk.starting;
		//
		//				if (this.isBeforeNow (startingDate)) {
		//					bookedWorkhopList.Add (wk);
		//				} else
		//					bookingWorkShopList.Add (wk);
		//			}
		//		}

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

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return workshopBookingList.Count;
			//			if (section == 0) {
			//				if (bookingWorkShopList.Count == 0) {
			//					return 1;
			//				}
			//				return bookingWorkShopList.Count;
			//			}
			//			else
			//				if (bookedWorkhopList.Count == 0) {
			//					return 1;
			//				}
			//				return bookedWorkhopList.Count;
		}


		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			//			Console.WriteLine (section + " section");
			//			if (section == 0) {
			//				return "Booking workshop";
			//			}
			//			else
			//			return "Booked workshop";

			return "Booking workshop";
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
				if (workshopBookingList.Count == 0) {
					dCell.TextLabel.Text = "No Records";
					return dCell;
				} else {
					cell.UpdateCell (workshopBookingList [indexPath.Row].topic + ""
						, workshopBookingList [indexPath.Row].starting + "");
					
				}

			}
			return cell;
		}

			/// <summary>
			/// Called when a row is touched
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				WorkshopBooking workshopBooking = workshopBookingList[indexPath.Row];

				myBookingDetailViewController = (MyBookingDetailViewController)AppDelegate.Storyboard.InstantiateViewController ("MyBookingDetailViewController");

				//myBookingDetailViewController
				if (workshopBooking != null) {
					myBookingDetailViewController.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
					myBookingDetailViewController.workshopBooking = workshopBooking;
					myBookingViewController.PresentViewController (myBookingDetailViewController, true, null);
				}
				//deselect row
				tableView.DeselectRow (indexPath, true);
			}
		}
	}

