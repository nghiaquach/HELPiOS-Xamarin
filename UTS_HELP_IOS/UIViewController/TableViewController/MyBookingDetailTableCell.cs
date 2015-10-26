using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace HELPiOS
{
	public class MyBookingDetailTableCell : UITableViewCell
	{
		UILabel titleLabel, valueLabel;

		public MyBookingDetailTableCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;
			Accessory = UITableViewCellAccessory.None;

			//ContentView.BackgroundColor = UIColor.FromRGB (218, 255, 127);

			titleLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica-Bold", 12f),
				TextAlignment = UITextAlignment.Left,
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

			valueLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica Neue", 12f),
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear
			};
					

			ContentView.Add (titleLabel);
			ContentView.Add (valueLabel);

		}

		public void UpdateCell (string title, string value)
		{
			//imageView.tex = image;
			titleLabel.Text = title;
			valueLabel.Text = value;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			//CGRect (x,y,w,h)
			titleLabel.Frame = new CGRect(5, 5, 90,35);
			valueLabel.Frame = new CGRect(80, 2, ContentView.Bounds.Width-90, 35);
		}
	}
}

