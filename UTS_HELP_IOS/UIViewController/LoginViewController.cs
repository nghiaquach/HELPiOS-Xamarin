
using System;

using Foundation;
using UIKit;

namespace HELPiOS
{
	public partial class LoginViewController : UIViewController
	{
		public LoginViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Console.WriteLine ("Start Login View");

			//When we push the button
			LoginButton.TouchUpInside += (o, e) => {
				//System out the text when user presses the button
				Console.WriteLine(StudentID.Text);
				Console.WriteLine(Password.Text);
				Console.WriteLine("Login Button Pressed!");
				//this.NavigationController.PushViewController (pinkViewController, true);
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

