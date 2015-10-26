// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HELPiOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet ("loginButton")]
		UIKit.UIButton LoginButton { get; set; }

		[Outlet ("password")]
		UIKit.UITextField Password { get; set; }

		[Outlet ("studentID")]
		UIKit.UITextField StudentID { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}

			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}

			if (StudentID != null) {
				StudentID.Dispose ();
				StudentID = null;
			}
		}
	}
}
