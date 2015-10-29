using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPiOS
{
    public class WorkshopBookingList
    {
        private const string apiBookingUri = "workshop/booking/";
        private const string apiWaitingUri = "workshop/wait/";
        private DataFacade db;

        public WorkshopBookingList()
        {
            db = new DataFacade();
        }

        private async Task create(SingleWorkshop workshop, Student student, string apiUri) {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("workshopId", workshop.WorkshopId);
            parameters.Add("studentId", student.studentID);
            parameters.Add("userId", student.studentID);
            Response<Object> response = await db.post<Object>(apiUri + "create", parameters, null);
            if (!response.IsSuccess)
                throw new StudentNotBooked(response.DisplayMessage);
        }

        public async Task createBooking(SingleWorkshop workshop, Student student)
        {
            await create(workshop, student, apiBookingUri);
        }

        public async Task createWaiting(SingleWorkshop workshop, Student student)
        {
            await create(workshop, student, apiWaitingUri);
        }

        public async Task cancel(SingleWorkshop workshop, Student student, string apiUri)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("workshopId", workshop.WorkshopId);
            parameters.Add("studentId", student.studentID);
            parameters.Add("userId", student.studentID);
            Response<Object> response = await db.post<Object>(apiUri + "cancel", parameters, null);
            if (!response.IsSuccess)
                throw new BookingNotCancelled(response.DisplayMessage);
        }

        public async Task cancelBooking(SingleWorkshop workshop, Student student)
        {
            await cancel(workshop, student, apiBookingUri);
        }

        public async Task cancelWaiting(SingleWorkshop workshop, Student student)
        {
            await cancel(workshop, student, apiWaitingUri);
        }

        private async Task<List<WorkshopBooking>> getByStudent(Student student, string dateFilter)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("studentId", student.studentID);
            parameters.Add(dateFilter, DateTime.Now);
            parameters.Add("active", true);
            Response<WorkshopBooking> response = await db.get<WorkshopBooking>(apiBookingUri + "search", parameters, null);
            if (!response.IsSuccess)
                throw new WebserviceFailureException(response.DisplayMessage);
            return response.Results;
        }

        public async Task<List<WorkshopBooking>> getUpcomingByStudent(Student student)
        {
            return await getByStudent(student, "endingDtBegin");
        }

        public async Task<List<WorkshopBooking>> getPastByStudent(Student student)
        {
            return await getByStudent(student, "endingDtEnd");
        }
    }
}