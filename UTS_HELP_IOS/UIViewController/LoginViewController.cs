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
				//do login
				string studentId = StudentID.Text;
				string password = Password.Text;

				if(validation(studentId,password)){
					this.doLogin(studentId,password);
				}
				else{
					AppParam.Instance.showAlertMessage("Login Fail","Please enter studentId or Password");
				}
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewDidAppear (bool animate){
			AppParam.Instance.student = null;

			StudentID.Text = "11875360";
			Password.Text = "1234567";
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

		private async void doLogin(string studentID, string password){
//for testing
//			this.doRegistration(studentID);


			//loading 
			LoadingOverlay.Instance.showLoading (this);

			StudentList studentList = new StudentList ();


			bool loginStatus = await studentList.login(studentID,password);

			if (loginStatus) {

				//Get student profile from api
				LoadingOverlay.Instance.showLoading (this);
				Student std = await studentList.getById (studentID);

				if (std != null) {
					AppParam.Instance.student = std;
					this.showNext ();
				} else {
					//show register screen
					this.doRegistration(studentID);
				}
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

		//do registration
		private void doRegistration(string studentID){
			RegisterViewController registerViewController = (RegisterViewController)AppDelegate.Storyboard.InstantiateViewController ("RegisterViewController");
			registerViewController.currentStudentID = studentID;
			this.PresentViewController(registerViewController, true, null);
		}
	}
}

