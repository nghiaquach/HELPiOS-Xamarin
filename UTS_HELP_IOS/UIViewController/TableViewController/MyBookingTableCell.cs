using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace HELPiOS
{
	public class MyBookingTableCell : UITableViewCell
	{
		UILabel topicLabel, startingDateLabel;

		public MyBookingTableCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Default;
			Accessory = UITableViewCellAccessory.DisclosureIndicator;

			//ContentView.BackgroundColor = UIColor.FromRGB (218, 255, 127);

			topicLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica-Bold", 13f),

				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

			startingDateLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica Neue", 12f),
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear
			};

			ContentView.Add (topicLabel);
			ContentView.Add (startingDateLabel);

		}

		public void UpdateCell (string caption, string subtitle)
		{
			//imageView.tex = image;
			topicLabel.Text = caption;
			startingDateLabel.Text = subtitle;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			//CGRect (x,y,w,h)
			topicLabel.Frame = new CGRect(10, 1, ContentView.Bounds.Width-20, 35);
			startingDateLabel.Frame = new CGRect(10, 30, ContentView.Bounds.Width-20, 20);
		}
	}
}

