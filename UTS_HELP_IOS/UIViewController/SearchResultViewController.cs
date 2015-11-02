
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;

namespace HELPiOS
{
	public partial class SearchResultViewController : UIViewController
	{
		public HashSet<Campus> campusHashSet{ get; set;}
		public HashSet<Lecturer> lectureHashSet{ get; set;}

		private List<Campus> campusList = new List<Campus>();
		private List<Lecturer> lecturerList = new List<Lecturer>();

		public Campus selectedCampus{ get; set;}
		public Lecturer selectedLecture{ get; set;}

		public NewBookingViewController newBookingViewController;

		public SearchResultViewController (IntPtr handle) : base (handle)
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

			//handle back button action
			backButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};
		}

		public override void ViewDidAppear (bool animated)
		{
			this.loadDataResultTable ();
		}
						
		private void loadDataResultTable (){
			if (campusHashSet != null) {

				HashSet<Campus>.Enumerator e = campusHashSet.GetEnumerator();

				while (e.MoveNext())
				{
					campusList.Add(e.Current);
				}
			}

			if (lectureHashSet != null) {
				HashSet<Lecturer>.Enumerator l = lectureHashSet.GetEnumerator();

				while (l.MoveNext())
				{
					lecturerList.Add(l.Current);
				}
			}

			ResultTableSource rts = new ResultTableSource (this);
			rts.campusList = campusList;
			rts.lecturerList = lecturerList;

			searchResultTable.Source = rts;

			searchResultTable.ReloadData ();
			// Perform any additional setup after loading the view, typically from a nib.


		}
	}
}

