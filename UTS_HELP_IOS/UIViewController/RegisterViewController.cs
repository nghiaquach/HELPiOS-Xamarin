
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace HELPiOS
{
	public partial class RegisterViewController : UIViewController
	{
		public string currentStudentID { get; set;}
		public UIPickerView myPickerView, customPickerView;
		UIView currentPicker;
		public UITextField degreeTextField { get; set;}
		public UITextField statusTextField { get; set;}
		public UITextField educationTextField { get; set;}
		public UITextField educationBackgroundTextField { get; set;}

		public static string [] pickerTypeArr = new string [] {
			"Degree",
			"Status",
			"Education"
		};


		public RegisterViewController (IntPtr handle) : base (handle)
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

			degreeTextField = DegreeText;
			statusTextField = StatusText;
			educationTextField = EducationBackground;

			//handle back button action
			backButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync (true);
			};

			//check null for my profile
			if (AppParam.Instance.student != null) {

				navBar.TopItem.Title = "My Profile";

				Student tmpStudent = AppParam.Instance.student;
				degreeTextField.Text = "Ungraduate";
				statusTextField.Text = "Permanent";
				educationTextField.Text = "HSC";
				PreferedName.Text = tmpStudent.preferred_name;
				FirstLanguage.Text = tmpStudent.first_language;
				CountryOrigin.Text = "Australia";

				degreeTextField.UserInteractionEnabled = false;
				statusTextField.UserInteractionEnabled = false;
				educationTextField.UserInteractionEnabled = false;
				PreferedName.UserInteractionEnabled = false;
				FirstLanguage.UserInteractionEnabled = false;
				CountryOrigin.UserInteractionEnabled = false;

				SubmitButton.Hidden = true;


			} else {

				//Init degree picker
				CreatePicker ();
				// Perform any additional setup after loading the view, typically from a nib.

				degreeTextField.TouchDown += (object sender, EventArgs e) => { 
					myPickerView.Model = new PickerModel (this, pickerTypeArr [0]);
					ShowPicker (myPickerView);
				};

				statusTextField.TouchDown += (object sender, EventArgs e) => { 
					myPickerView.Model = new PickerModel (this, pickerTypeArr [1]);
					ShowPicker (myPickerView);
				};

				educationTextField.TouchDown += (object sender, EventArgs e) => { 
					myPickerView.Model = new PickerModel (this, pickerTypeArr [2]);
					ShowPicker (myPickerView);
				};

				this.SubmitButton.TouchUpInside += (o, e) => {

					Student std = new Student ();

					std.studentID = this.currentStudentID;
					std.preferred_name = PreferedName.Text;
//				std.degree = degreeTextField.Text.Equals("Undergraduate")?Degree.UnderGrad:Degree.PostGrad;
					std.degree = std.degree;
					std.status = std.status;
//				std.status = statusTextField.Text.Equals("International")?Status.International:Status.Permanent;
					std.first_language = FirstLanguage.Text;
					std.HSC = true;
					std.creatorId = AppParam.CreatorId;

					doRegister (std);

				};
			}
		}

		private async void doRegister(Student std){
			LoadingOverlay.Instance.showLoading(this);
			StudentList stdList = new StudentList();
			try{
				await stdList.create(std);
				this.showNext();
			}
			catch(Exception ex){
				AppParam.Instance.showAlertMessage("Register","Registration Fail!");
			}
		}

		private void showNext(){
			//this.NavigationController.PushViewController (pinkViewController, true);
			AppDelegate.mainTabbarController = (UITabBarController)AppDelegate.Storyboard.InstantiateViewController ("MainTabbarController");
			//AppDelegate.mainTabbarController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
			this.PresentViewController(AppDelegate.mainTabbarController, true, null);
		}

		void CreatePicker ()
		{
			//
			// Empty is used, since UIPickerViews have auto-sizing,
			// all that is required is the origin
			//
			myPickerView = new UIPickerView (CGRect.Empty){
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
				ShowSelectionIndicator = true,
				Hidden = true
			};
			//(240,248,255)
			myPickerView.BackgroundColor = UIColor.FromRGB (240,248,255);
			// Now update it:
			myPickerView.Frame = PickerFrameWithSize (myPickerView.SizeThatFits (CGSize.Empty));
			View.AddSubview (myPickerView);
		}


		void ShowPicker (UIView picker)
		{
			if (currentPicker != null){
				currentPicker.Hidden = true;
			}

			picker.Hidden = false;
			currentPicker = picker;
		}

		CGRect PickerFrameWithSize (CGSize size)
		{
			var screenRect = UIScreen.MainScreen.ApplicationFrame;
			return new CGRect (0f, screenRect.Height - 84f - size.Height, size.Width, size.Height);
		}

	}


	class PickerModel : UIPickerViewModel {
		

		static string [] educationArr = new string [] {
			"HSC",
			"HSCMark",
			"IELTS",
			"IELTSMark",
			"TOEFL",
			"TOEFLMark",
			"TAFE",
			"TAFEMark",
			"CULT",
			"CULTMark",
			"InsearchDEEP",
			"InsearchDEEPMark",
			"InsearchDiploma",
			"InsearchDiplomaMark",
			"FoundationCourse",
			"FoundationCourseMark"
		};


		static string [] statusArr = new string [] {
			"Permanent",
			"International"
		};

		static string [] degreeArr = new string [] {
			"Undergraduate",
			"Postgraduate"
		};

		public static string [] pickerTypeArr = new string [] {
			"Degree",
			"Status",
			"Education"
		};
			

		RegisterViewController pvc;
		String pickerType;
		public PickerModel (RegisterViewController pvc, string pickerType) {
			this.pvc = pvc;
			this.pickerType = pickerType;
		}

		public override nint GetComponentCount (UIPickerView v)
		{
			return 1;
		}

		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			
			if (pickerType.Equals (pickerTypeArr[0])) {
				return degreeArr.Length;
			}
			else if (pickerType.Equals(pickerTypeArr[1])){
				return statusArr.Length;
			}
			else
				return educationArr.Length;

		}

		public override string GetTitle (UIPickerView picker, nint row, nint component)
		{
			if (pickerType.Equals (pickerTypeArr[0])) {
				return degreeArr[row];
			}
			else if (pickerType.Equals(pickerTypeArr[1])){
				return statusArr[row];
			}
			else
				return educationArr[row];
		}

		public override void Selected (UIPickerView picker, nint row, nint component)
		{
			if (pickerType.Equals (pickerTypeArr[0])) {
				pvc.degreeTextField.Text =  (degreeArr[row]);
			}
			else if (pickerType.Equals(pickerTypeArr[1])){
				pvc.statusTextField.Text =  (statusArr[row]);
			}
			else
				pvc.educationTextField.Text =  (educationArr[row]);
			
			pvc.myPickerView.Hidden = true;
		}

		public override nfloat GetComponentWidth (UIPickerView picker, nint component)
		{
			if (component == 0)
				return 240f;
			else
				return 40f;
		}

		public override nfloat GetRowHeight (UIPickerView picker, nint component)
		{
			return 40f;
		}
	}

}


