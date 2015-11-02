using System;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace HELPiOS
{
	public class ResultTableSource : UITableViewSource
	{
		private List<Campus> campusList;
		private List<Lecturer> lecturerList;

		string CellIdentifier = "TableCell";
		private SearchResultViewController searchResultViewController;

		public ResultTableSource (SearchResultViewController se, List<Campus> campusList,
			List<Lecturer> lecturerList)
		{
			this.searchResultViewController = se;
			this.campusList = campusList;
			this.lecturerList = lecturerList;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (campusList != null) {
				return campusList.Count;
			} else {
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

			if (campusList != null) {
				c = campusList [indexPath.Row];
				cell.TextLabel.Text = c.campus;
				return cell;
			} else {
				l = lecturerList [indexPath.Row];
				cell.TextLabel.Text = l.first_name + " " + l.last_name;
				return cell;
			}

		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
		}
	}
}

