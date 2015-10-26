using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		}
			

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return workshopBookingList.Count;
		}


		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
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
			public async override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				WorkshopBooking workshopBooking = workshopBookingList[indexPath.Row];

				myBookingDetailViewController = (MyBookingDetailViewController)AppDelegate.Storyboard.InstantiateViewController ("MyBookingDetailViewController");

				//myBookingDetailViewController
				if (workshopBooking != null) {
					AppParam.campustName = await this.getCampusRoom (workshopBooking.campusID);
					myBookingDetailViewController.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
					myBookingDetailViewController.workshopBooking = workshopBooking;
					myBookingViewController.PresentViewController (myBookingDetailViewController, true, null);
				}
				//deselect row
				tableView.DeselectRow (indexPath, true);
			}

			private async Task<string> getCampusRoom(int campusId){
				CampusList campusList = new CampusList ();
				Campus campus = await campusList.getById (campusId);
				if (campus == null) {
					return "";
				}
				return campus.campus;
			}

		}
	}

