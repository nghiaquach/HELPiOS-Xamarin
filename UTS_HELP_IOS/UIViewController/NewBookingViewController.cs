
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HELPiOS
{
	public partial class NewBookingViewController : UIViewController
	{

		enum SearchSelection
		{
			Topic,
			Date,
			Location,
			Tutor
		};

		private SearchSelection selectedItem;
		SearchResultViewController searchResultViewController;
		DateViewController dateVC;
		private DateTime nsRef = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

		public NewBookingViewController () : base ("NewBookingViewController", null)
		{
		}

		public NewBookingViewController (IntPtr handle) : base (handle)
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

			searchButton.Enabled = false;
			// Perform any additional setup after loading the view, typically from a nib.
			segmentControl.ValueChanged += (sender, e) => {
				var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
				switch (selectedSegmentId){
				case 0:
					selectedItem = SearchSelection.Topic;
					searchResultViewController = null;
					break;
				case 1:
					selectedItem = SearchSelection.Date;
					searchResultViewController = null;
					break;
				case 2:
					selectedItem = SearchSelection.Location;
					break;
				case 3:
					selectedItem = SearchSelection.Tutor;
					break;
				}

				searchBar.Text = "";
				searchButton.Enabled = false;

				resultTableView.Source = null;
				resultTableView.ReloadData();
			};

			searchBar.TextChanged += (s, e) => {

				if(searchBar.Text.Equals("")){
					searchButton.Enabled = false;
				}

				if (selectedItem == SearchSelection.Date){
					//this.NavigationController.PushViewController (pinkViewController, true);
					if(dateVC==null){
						dateVC = (DateViewController)AppDelegate.Storyboard.InstantiateViewController ("DateViewController");
					}
					//AppDelegate.mainTabbarController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
					this.PresentViewController(dateVC, true, null);
				}

				if(!searchBar.Text.Equals(""))
				searchButton.Enabled = true;
			};

//			searchBar.Sho += (s, e) => {
//				if(!searchBar.Text.Equals(""))
//				searchButton.Enabled = true;
//			};


			searchButton.Clicked += (o, e)  => {
				Console.WriteLine("searchBar: "+searchBar.Text);

				if(selectedItem == SearchSelection.Topic){
					this.searchByTopic();
				}
				else if(selectedItem == SearchSelection.Date){
					this.searchByDate();
				}
				else if(selectedItem == SearchSelection.Location){
					this.searchByLocation();
				}
				else if(selectedItem == SearchSelection.Tutor){
					this.searchByLecturer();
				}

			};

		}

		public override void ViewDidAppear (bool animate){
			// Perform any additional setup after loading the view, typically from a nib.
			if (dateVC != null) {
				if (dateVC.selectedDate != null) {
					searchBar.Text = dateVC.selectedDate.ToString ();
					searchButton.Enabled = true;
				}
			}
			this.doSearch ();

		}

		private async void doSearch(){
			if (searchResultViewController != null) {
				Campus tmpCampus = searchResultViewController.selectedCampus;
				Lecturer tmpLecturer = searchResultViewController.selectedLecture;
				List<SessionBooking> ssList = new List<SessionBooking> ();

				if (tmpCampus != null) {
					ssList = await this.searchByCampus (tmpCampus);
				}
				if (tmpLecturer != null) {
					ssList = await this.searchByLecturer (tmpLecturer);
				}

				this.showResultInTable (ssList);
			}
		}
			

		private async Task<List<SessionBooking>> searchByCampus(Campus campus){
			LoadingOverlay.Instance.showLoading(this);
			SessionBookingList ssList = new SessionBookingList();
			return await ssList.searchByLocation (campus);
		}

		private async Task<List<SessionBooking>> searchByLecturer(Lecturer lecturer){
			LoadingOverlay.Instance.showLoading(this);
			SessionBookingList ssList = new SessionBookingList();
			return await ssList.searchByLecturer (lecturer);
		}
		//search by topic
		private async void searchByTopic(){
			LoadingOverlay.Instance.showLoading(this);
			if (!searchBar.Text.Equals("")) {
				WorkshopList workshopList = new WorkshopList();
				List<SingleWorkshop> ab =  await workshopList.searchByTopic(searchBar.Text);
			
				Console.WriteLine("Result "+ ab.Count);
			}
		}

		//search by date
		private async void searchByDate(){
			LoadingOverlay.Instance.showLoading(this);
			if (!searchBar.Text.Equals("")) {
				WorkshopList workshopList = new WorkshopList();

				List<SingleWorkshop> ab =  await workshopList.searchByStartDate(this.toDateFromNSDate(dateVC.selectedDate));
				this.showResultInTable (ab);
			}
		}

		//search by date
		private async void searchByLocation(){
			LoadingOverlay.Instance.showLoading(this);
			if (!searchBar.Text.Equals("")) {

				CampusList campusList = new CampusList ();
				HashSet<Campus> campuses = await campusList.filterByName (searchBar.Text);

				showTempResultTable (campuses,null);
			}
		}

		//search by date
		private async void searchByLecturer(){
			LoadingOverlay.Instance.showLoading(this);
			if (!searchBar.Text.Equals("")) {

				LecturerList lecturerList = new LecturerList ();
				HashSet<Lecturer> lecturers = await lecturerList.filterByName (searchBar.Text);
				showTempResultTable (null,lecturers);
			}
		}

		/// <summary>Convert an NSDate to DateTime</summary>
		/// <param name="nsDate">The NSDate to convert</param>
		/// <returns>A DateTime</returns>
			public DateTime toDateFromNSDate(NSDate nsDate) {
			// We loose granularity below millisecond range but that is probably ok
			return nsRef.AddSeconds(nsDate.SecondsSinceReferenceDate);
		}

		public void showResultInTable(List<SessionBooking> ssList){
			resultTableView.Source = new NewBookingTableSource (this,ssList);
			resultTableView.ReloadData ();
		}

		public void showResultInTable(List<SingleWorkshop> singletWorkshopList){
			resultTableView.Source = new NewBookingTableSource (this,singletWorkshopList);
			resultTableView.ReloadData ();
		}


		private void showTempResultTable(HashSet<Campus> campusHashSet, HashSet<Lecturer> lecturerHashSet){
			searchResultViewController = (SearchResultViewController)AppDelegate.Storyboard.InstantiateViewController ("SearchResultViewController");
			searchResultViewController.campusHashSet = campusHashSet;
			searchResultViewController.lectureHashSet = lecturerHashSet;
			searchResultViewController.newBookingViewController = this;
			this.PresentViewController(searchResultViewController, true, null);
		}


	}


}

