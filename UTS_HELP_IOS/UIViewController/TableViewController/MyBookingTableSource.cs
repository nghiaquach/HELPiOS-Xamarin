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
		List<SessionBooking> sessionBookingList = new List<SessionBooking>();

		MyBookingDetailViewController myBookingDetailViewController;
		UIViewController myBookingViewController;

		NSString cellIdentifier = new NSString("TableCell");

		public MyBookingTableSource (UIViewController myBookingViewController,
			List<WorkshopBooking> workshopBookingList, List<SessionBooking> sessionBookingList)
		{
			this.workshopBookingList = workshopBookingList;
			this.myBookingViewController = myBookingViewController;
			this.sessionBookingList = sessionBookingList;
		}
			

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (section == 0) {
				return workshopBookingList.Count;
			}
			else
				return sessionBookingList.Count;
		}


		public override nint NumberOfSections (UITableView tableView)
		{
			return 2;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			if (section == 0) {
				return "Booking workshop";
			}
			else
				return "Booking session";
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

			if (indexPath.Section == 1) {
				if (sessionBookingList.Count == 0) {
					dCell.TextLabel.Text = "No Records";
					return dCell;
				} else {
					cell.UpdateCell (sessionBookingList [indexPath.Row].SessionType + ""
						, sessionBookingList [indexPath.Row].StartDate + "");

				}

			}
			return cell;
		}

			/// <summary>
			/// Called when a row is touched
			/// </summary>
			public async override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
			
					if (myBookingDetailViewController == null) {
				myBookingDetailViewController = (MyBookingDetailViewController)AppDelegate.Storyboard.InstantiateViewController ("MyBookingDetailViewController");
					}
					
					if (indexPath.Section == 0) {
						WorkshopBooking workshopBooking = workshopBookingList [indexPath.Row];
						//myBookingDetailViewController
						if (workshopBooking != null) {
							LoadingOverlay.Instance.showLoading (myBookingViewController);
							AppParam.campusName = await this.getCampusRoom (workshopBooking.campusID);
							myBookingDetailViewController.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
							myBookingDetailViewController.wkBooking = workshopBooking;
							myBookingDetailViewController.ssBooking = null;
							myBookingViewController.PresentViewController (myBookingDetailViewController, true, null);
						}
					} else {
						SessionBooking sessionBooking = sessionBookingList [indexPath.Row];
						//myBookingDetailViewController
						if (sessionBooking != null) {
							myBookingDetailViewController.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
							myBookingDetailViewController.wkBooking = null;
							myBookingDetailViewController.ssBooking = sessionBooking;
							myBookingViewController.PresentViewController (myBookingDetailViewController, true, null);
						}
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

