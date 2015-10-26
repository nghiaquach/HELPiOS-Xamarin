using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
    public class WorkshopList
	{
		private const string apiUri = "workshop/";
		private DataFacade db;

		public WorkshopList()
		{
			db = new DataFacade();
		}

        public async Task<AbstractWorkshop> getById(int id)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("active", true);
            Response<AbstractWorkshop> response = await db.get<AbstractWorkshop>(apiUri + "/search", parameters, new AbstractWorkshopConverter());
            if (response.IsSuccess)
            {
                foreach (AbstractWorkshop workshop in response.Results)
                {
                    if (workshop.WorkshopId == id)
                        return workshop;
                }
                throw new WorkshopNotFound(response.DisplayMessage);
            }
            else
		{
                throw new WebserviceFailureException(response.DisplayMessage);
            }
		}

        private async Task<List<AbstractWorkshop>> search(Dictionary<string, Object> parameters)
        {
            if (!parameters.ContainsKey("startingDtBegin"))
                parameters.Add("startingDtBegin", DateTime.Now);
            parameters.Add("active", true);
            Response<AbstractWorkshop> response = await db.get<AbstractWorkshop>(apiUri + "/search", parameters, new AbstractWorkshopConverter());
            if (response.IsSuccess && response.Results != null)
            {
                return response.Results;
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }

        public async Task<List<AbstractWorkshop>> search()
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            return await search(parameters);
        }

        public async Task<List<AbstractWorkshop>> searchByTopic(string topic)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("topic", topic);
            return await search(parameters);
        }

        public async Task<List<AbstractWorkshop>> searchByStartDate(DateTime date)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("startingDtBegin", date);
            parameters.Add("startingDtEnd", date);
            return await search(parameters);
        }

        public async Task<List<AbstractWorkshop>> searchByLocation(string location)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("campusID", new CampusList().filterByName(location));
            return await search(parameters);
        }

        public async Task<List<AbstractWorkshop>> searchByLecturer(string lecturer)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("lecturer", lecturer);
            return await search(parameters);
        }
	}
}
