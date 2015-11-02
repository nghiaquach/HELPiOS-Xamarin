
using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using System.Collections.Generic;
using EventKit;

namespace HELPiOS
{
	public partial class MyBookingDetailViewController : UIViewController
	{
		public WorkshopBooking wkBooking { get; set;}
		public SessionBooking ssBooking { get; set;}

		protected CreateEventEditViewDelegate eventControllerDelegate;
		// screens
		protected CalendarListController calendarListScreen;
		
		public MyBookingDetailViewController (IntPtr handle) : base (handle)
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
			//disable buttons for session booking
			if (ssBooking != null) {
				cancelButton.Enabled = false;
			} else {
				cancelButton.Enabled = true;
			}
			//disable edit for text field
			descriptionTextView.Editable = false;

			//handle back button action
			backButton.Clicked += (o, e) => {
				this.DismissViewControllerAsync(true);
			};

			//handle cancel button action
			cancelButton.TouchUpInside += (o, e) => {

				this.cancelWorkshopBooking();
				LoadingOverlay.Instance.showLoading(this);
			};

			//handle set reminder button action
			setReminderButton.Clicked += (o, e) => {
				showSetReminder();
			};

//			Console.WriteLine ("Load My Bookings Detail View");
			this.showBookingDetail ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void showBookingDetail ()
		{		
			if (wkBooking != null) {
				cancelButton.Hidden = false;
				descriptionTextView.Hidden = false;
				descriptionTextView.Text = wkBooking.description == null ? "No description" : wkBooking.description;
			} else {
				cancelButton.Hidden = true;
				descriptionTextView.Hidden = true;
			}
			
			myBookingDetailTable.Source = new MyBookingDetailTableSource (wkBooking,ssBooking);
			myBookingDetailTable.ReloadData ();
		}

		private async void cancelWorkshopBooking(){
			if (wkBooking != null) {
				WorkshopBookingList workshopBookingList = new WorkshopBookingList();
					try {
						await workshopBookingList.cancelBooking (wkBooking, AppParam.Instance.student);
						this.DismissViewControllerAsync (true);
					} catch (Exception ex) {
						AppParam.Instance.showAlertMessage ("Workshop Booking", "Cancel workshop booking Fail!");
					}
			}
		}

		private void showSetReminder(){			
			RequestAccess (EKEntityType.Event, () => {
				LaunchModifyEvent ();
			});
		}

		/// <summary>
		/// Launchs the create new event controller.
		/// </summary>
		protected void LaunchModifyEvent ()
		{
			// first we need to create an event it so we have one that we know exists
			// in a real world scenario, we'd likely either a) be modifying an event that
			// we found via a query, or 2) we'd do like this, in which we'd automatically
			// populate the event data, like for say a dr. appt. reminder, or something
			EKEvent newEvent = EKEvent.FromStore (CalendarParam.Current.EventStore);
			// set the alarm for 10 minutes from now
			newEvent.AddAlarm (EKAlarm.FromDate ((NSDate)DateTime.FromFileTime(wkBooking.starting.ToFileTime())));
			// make the event start 20 minutes from now and last 30 minutes
			newEvent.StartDate = (NSDate)DateTime.Now.AddMinutes (20);
			newEvent.EndDate = (NSDate)DateTime.Now.AddMinutes (50);
			newEvent.Title = wkBooking.topic;
			newEvent.Location = AppParam.campusName;
			newEvent.Notes = wkBooking.description;


			// create a new EKEventEditViewController. This controller is built in an allows
			// the user to create a new, or edit an existing event.
			EventKitUI.EKEventEditViewController eventController =
				new EventKitUI.EKEventEditViewController ();

			// set the controller's event store - it needs to know where/how to save the event
			eventController.EventStore = CalendarParam.Current.EventStore;
			eventController.Event = newEvent;

			// wire up a delegate to handle events from the controller
			eventControllerDelegate = new CreateEventEditViewDelegate (eventController);
			eventController.EditViewDelegate = eventControllerDelegate;

			// show the event controller
			PresentViewController (eventController, true, null);
		}

		protected class CreateEventEditViewDelegate : EventKitUI.EKEventEditViewDelegate
		{
			// we need to keep a reference to the controller so we can dismiss it
			protected EventKitUI.EKEventEditViewController eventController;

			public CreateEventEditViewDelegate (EventKitUI.EKEventEditViewController eventController)
			{
				// save our controller reference
				this.eventController = eventController;
			}

			// completed is called when a user eith
			public override void Completed (EventKitUI.EKEventEditViewController controller, EventKitUI.EKEventEditViewAction action)
			{
				eventController.DismissViewController (true, null);

				// action tells you what the user did in the dialog, so you can optionally
				// do things based on what their action was. additionally, you can get the
				// Event from the controller.Event property, so for instance, you could
				// modify the event and then resave if you'd like.
				switch (action) {

				case EventKitUI.EKEventEditViewAction.Canceled:
					break;
				case EventKitUI.EKEventEditViewAction.Deleted:
					break;
				case EventKitUI.EKEventEditViewAction.Saved:
					// if you wanted to modify the event you could do so here, and then
					// save:
					//App.Current.EventStore.SaveEvent ( controller.Event, )
					break;
				}
			}
		}

		protected void RequestAccess (EKEntityType type, System.Action completion)
		{
			CalendarParam.Current.EventStore.RequestAccess (type,
				(bool granted, NSError e) => {
					InvokeOnMainThread (() => {
						if (granted)
							completion.Invoke();
						else
							new UIAlertView ("Access Denied", "User Denied Access to Calendars/Reminders", null, "ok", null).Show ();
					});
				});
		}
	}
}

