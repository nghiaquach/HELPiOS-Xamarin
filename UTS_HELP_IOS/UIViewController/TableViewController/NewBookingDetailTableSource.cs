using System;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace HELPiOS
{
	public class NewBookingDetailTableSource : UITableViewSource {

		SingleWorkshop singleWorkshop;
		SessionBooking sessionBooking;

		NSString cellIdentifier = new NSString("TableCell");

		public NewBookingDetailTableSource (SingleWorkshop singleWorkshop)
		{
			this.singleWorkshop = singleWorkshop;
		}


		public NewBookingDetailTableSource (SessionBooking sessionBooking)
		{
			this.sessionBooking = sessionBooking;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return 5;
		}


		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}


		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{

			// request a recycled cell to save memory
			MyBookingDetailTableCell cell = tableView.DequeueReusableCell (cellIdentifier) as MyBookingDetailTableCell;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new MyBookingDetailTableCell (cellIdentifier);
			}

			if (singleWorkshop != null) {
				switch (indexPath.Row) {
				case 0:
					cell.UpdateCell (@"Title", singleWorkshop.topic);
					break;
				case 1:
					cell.UpdateCell (@"Room", singleWorkshop.campus + "");
					break;
				case 2:
					cell.UpdateCell (@"Date Time", singleWorkshop.StartDate + "");
					break;
				case 3:
					cell.UpdateCell (@"Target Group", singleWorkshop.targetingGroup == null ? "None" : singleWorkshop.targetingGroup);
					break;
				case 4:
					cell.UpdateCell (@"Place available", singleWorkshop.maximum + "");
					break;
				}
			}

			if (sessionBooking != null) {
				switch (indexPath.Row) {
				case 0:
					cell.UpdateCell (@"Title", sessionBooking.SessionType);
					break;
				case 1:
					cell.UpdateCell (@"Room", sessionBooking.Campus + "");
					break;
				case 2:
					cell.UpdateCell (@"Date Time", sessionBooking.StartDate + "");
					break;
				case 3:
					cell.UpdateCell (@"Target Group", sessionBooking.IsGroup == false ? "Single" : "Multiple");
					break;
				case 4:
					cell.UpdateCell (@"Place available", sessionBooking.NumPeople + "");
					break;
				}
			}

			return cell;
		}

	}
}

