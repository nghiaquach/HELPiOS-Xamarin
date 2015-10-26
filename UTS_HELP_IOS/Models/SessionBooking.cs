using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTS_HELP_ANDROID.Resources.Models
{
    public class SessionBooking
    {
        public int SessionId { get; set; }
        public string LecturerFirstName { get; set; }
        public string LecturerLastName { get; set; }
        public string LecturerEmail { get; set; }
        public string SessionTypeAbb { get; set; }
        public string SessionType { get; set; }
        public string AssignmentType { get; set; }
        public string AppointmentType { get; set; }
        public int BookingId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Campus { get; set; }
        public bool Cancel { get; set; }
        public string Assistance { get; set; }
        public string Reason { get; set; }
        public int Attended { get; set; }
        public int? WaitingID { get; set; }
        public bool IsGroup { get; set; }
        public int? NumPeople { get; set; }
        public string LecturerComment { get; set; }
        public string LearningIssues { get; set; }
        public DateTime? IsLocked { get; set; }
        public string AssignTypeOther { get; set; }
        public string Subject { get; set; }
        public string AppointmentsOther { get; set; }
    }
}