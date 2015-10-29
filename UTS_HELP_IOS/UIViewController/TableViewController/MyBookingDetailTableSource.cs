using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HELPiOS
{
	public class MyBookingDetailTableSource : UITableViewSource {

		WorkshopBooking workshopBooking;
		NSString cellIdentifier = new NSString("TableCell");

		public MyBookingDetailTableSource (WorkshopBooking workshopBooking)
		{
			this.workshopBooking = workshopBooking;
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
			//			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			//			WorkshopBooking workshopBooking = workshopBookingList[indexPath.Row];
			//
			//			//---- if there are no cells to reuse, create a new one
			//			if (cell == null)
			//			{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }
			//
			//			cell.TextLabel.Text = workshopBooking.topic;
			//
			//			return cell;

			// request a recycled cell to save memory

			// request a recycled cell to save memory
			MyBookingDetailTableCell cell = tableView.DequeueReusableCell (cellIdentifier) as MyBookingDetailTableCell;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new MyBookingDetailTableCell (cellIdentifier);
			}

			switch (indexPath.Row)
			{
			case 0:
				cell.UpdateCell (@"Title", workshopBooking.topic);
				break;
			case 1:
				cell.UpdateCell (@"Room", AppParam.campusName);
				break;
			case 2:
				cell.UpdateCell (@"Date Time", workshopBooking.starting+"");
				break;
			case 3:
				cell.UpdateCell (@"Target Group", workshopBooking.targetingGroup==null?"None":workshopBooking.targetingGroup);
				break;
			case 4:
				cell.UpdateCell (@"Place available", workshopBooking.maximum+"");
				break;
			}
			return cell;
		}
			
	}



}

