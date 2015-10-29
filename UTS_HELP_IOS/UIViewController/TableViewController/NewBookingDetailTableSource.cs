using System;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace HELPiOS
{
	public class NewBookingDetailTableSource : UITableViewSource {

		SingleWorkshop singleWorkshop;
		NSString cellIdentifier = new NSString("TableCell");

		public NewBookingDetailTableSource (SingleWorkshop singleWorkshop)
		{
			this.singleWorkshop = singleWorkshop;
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

			switch (indexPath.Row)
			{
			case 0:
				cell.UpdateCell (@"Title", singleWorkshop.topic);
				break;
			case 1:
				cell.UpdateCell (@"Room", singleWorkshop.campus+"");
				break;
			case 2:
				cell.UpdateCell (@"Date Time", singleWorkshop.StartDate+"");
				break;
			case 3:
				cell.UpdateCell (@"Target Group", singleWorkshop.targetingGroup==null?"None":singleWorkshop.targetingGroup);
				break;
			case 4:
				cell.UpdateCell (@"Place available", singleWorkshop.maximum+"");
				break;
			}

			return cell;
		}

	}
}

