using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPiOS
{
    public class SessionBookingList
    {
        private const string apiUri = "session/booking/";
        private DataFacade db;

    	public SessionBookingList()
	    {
            db = new DataFacade();
	    }

        private async Task<List<SessionBooking>> getByStudent(Student student, string dateFilter)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("studentId", student.studentID);
            parameters.Add(dateFilter, DateTime.Now);
            parameters.Add("active", true);
            Response<SessionBooking> response = await db.get<SessionBooking>(apiUri + "search", parameters, null);
            if (!response.IsSuccess)
                throw new WebserviceFailureException(response.DisplayMessage);
            return response.Results;
        }

        public async Task<List<SessionBooking>> getUpcomingByStudent(Student student)
        {
            return await getByStudent(student, "endingDtBegin");
        }

        public async Task<List<SessionBooking>> getPastByStudent(Student student)
        {
            return await getByStudent(student, "endingDtEnd");
        }
    }
}