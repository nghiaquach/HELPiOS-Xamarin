using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
	public partial class LoginViewController : UIViewController
	{
		
		public static HelpItemManager HelpManager { get; private set; }
//		LoadingOverlay loadingOverlay;

		public LoginViewController (IntPtr handle): base (handle)
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

//			//When we push the button
			this.LoginButton.TouchUpInside += (o, e) => {
				//for test
				this.showNext();
				Console.WriteLine("Login Button Pressed!");

				//do login
				string studentId = StudentID.Text;
				string password = Password.Text;

				Login login = new Login();
				login.studentId = studentId;
				login.password = password;

//				if(validation(studentId,password)){
//					this.doLogin(login);
//				}
//				else{
//					AppParam.Instance.showAlertMessage("Login Fail","Please enter studentId or Password");
//				}
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewDidAppear (bool animate){
			StudentID.Text = "";
			Password.Text = "";
		}

		private bool validation(string studentId, string password){
			if(studentId.Equals("")){
				return false;
			}

			if (password.Equals ("")) {
				return false;
			}
			return true;
		}

		private async void doLogin(Login login){

			LoadingOverlay.Instance.showLoading (this);

			HelpManager = new HelpItemManager (new RestService ());
			Task<bool> loginReponseTask = HelpManager.doLoginTasksAsync(login);
			bool loginStatus = await loginReponseTask;

			if (loginStatus) {
				this.showNext ();
			} else {
				AppParam.Instance.showAlertMessage ("Login Status","Student ID and Password are not match!");
			}
		}

		private void showNext(){
			//this.NavigationController.PushViewController (pinkViewController, true);
			AppDelegate.mainTabbarController = (UITabBarController)AppDelegate.Storyboard.InstantiateViewController ("MainTabbarController");

			//AppDelegate.mainTabbarController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
			this.PresentViewController(AppDelegate.mainTabbarController, true, null);
		}


//		public void showLoading(){
//			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
//			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
//				bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
//			}
//			// show the loading overlay on the UI thread using the correct orientation sizing
//			this.loadingOverlay = new LoadingOverlay (bounds);
//			this.View.Add ( this.loadingOverlay );
//		}
//
//		public void hideLoading(){
//			loadingOverlay.Hide ();
//		}



//			try{
//
//				Task<WorkshopBooking> workshopBookingTask = workshopList.getByStudentId ("11875360");
//				//WorkshopBooking workshopBooking = await workshopBookingTask;
//
//				//foreach (WorkshopBooking wb in workshopBooking){
//				//Console.WriteLine (workshopBooking.description);		
//				//}
//
//			}
//			catch(Exception exception){
//
//			}
//		}
	}
}

