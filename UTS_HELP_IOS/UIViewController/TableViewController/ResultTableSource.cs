using System;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace HELPiOS
{
	public class ResultTableSource : UITableViewSource
	{
		public List<Campus> campusList { get; set;}
		public List<Lecturer> lecturerList{ get; set;}

		string CellIdentifier = "TableCell";

		private SearchResultViewController searchResultViewController;

		public ResultTableSource (SearchResultViewController se)
		{
			this.searchResultViewController = se;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (campusList.Count>0) {
				return campusList.Count;
			} else {
				Console.WriteLine ("lecture list Count: " + lecturerList.Count);
				return lecturerList.Count;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }


			Campus c = null;
			Lecturer l = null;

			if (campusList.Count>0){
				c = campusList [indexPath.Row];
				cell.TextLabel.Text = c.campus;
				return cell;
			} else {
				l = lecturerList [indexPath.Row];
				cell.TextLabel.Text = l.first_name + " - " + l.last_name;
				return cell;
			}

		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (campusList.Count > 0) {
				searchResultViewController.selectedCampus = campusList [indexPath.Row];
			} else {
				searchResultViewController.selectedLecture = lecturerList [indexPath.Row];
			}
			tableView.DeselectRow (indexPath, true);
			searchResultViewController.DismissViewControllerAsync(true);
		}
	}
}

