using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HELPiOS
{
	public class HelpItemManager
	{
		IRestService restService;

		public HelpItemManager (IRestService service)
		{
			restService = service;
		}

//		public Task<List<Lecturers>> GetLecturesTasksAsync ()
//		{
//			
//			return restService.GetLecturerList ();	
//		}

		public Task<List<WorkshopBooking>> GetWorkshopBookingTasksAsync ()
		{

			return restService.GetWorkshopBookingList ();	
		}

		public Task<bool> doLoginTasksAsync (Login login)
		{

			return restService.doLogin (login);	
		}


	}
}
