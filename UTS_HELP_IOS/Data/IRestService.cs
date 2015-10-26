using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HELPiOS
{
	public interface IRestService
	{
		//Get lecturers list
		//Task<List<Lecturers>> GetLecturerList ();

		Task<List<WorkshopBooking>> GetWorkshopBookingList ();
		Task<bool> doLogin (Login login);
	}
}
