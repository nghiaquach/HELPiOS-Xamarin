using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
    public class WorkshopList
	{
		private const string apiUri = "workshop";
		private DataFacade db;

		public WorkshopList()
		{
			db = new DataFacade();
		}

        public async Task<T> getById<T>(int id) where T : SingleWorkshop
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("active", true);
            Response<T> response = await db.get<T>(apiUri + "/search", parameters, new AbstractWorkshopConverter());
            if (response.IsSuccess)
            {
                foreach (T workshop in response.Results)
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

        private async Task<List<SingleWorkshop>> search(Dictionary<string, Object> parameters)
        {
            if (!parameters.ContainsKey("startingDtBegin"))
            {
                parameters.Add("startingDtBegin", new DateTime());
                parameters.Add("startingDtEnd", DateTime.Now);
            }
            parameters.Add("active", true);
            Response<SingleWorkshop> response = await db.get<SingleWorkshop>(apiUri + "/search", parameters, null);
            if (response.IsSuccess && response.Results != null)
            {
                return response.Results;
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }

        public async Task<List<SingleWorkshop>> search()
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            return await search(parameters);
        }

        public async Task<List<SingleWorkshop>> searchByTopic(string topic)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("active", true);
			Response<SingleWorkshop> response = await db.get<SingleWorkshop>(apiUri + "/search", parameters, null);
			if (response.IsSuccess && response.Results != null)
			{
				List<SingleWorkshop> tmpList = new List<SingleWorkshop> ();

				foreach (SingleWorkshop singleWorkshop in response.Results)
				{
					if (singleWorkshop.topic.ToLower().Contains (topic.ToLower())) {
						tmpList.Add (singleWorkshop);
					}
				}
				return tmpList;
			}
			else
			{
				throw new WebserviceFailureException(response.DisplayMessage);
			}
        }

        public async Task<List<SingleWorkshop>> searchByStartDate(DateTime date)
        {
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("startingDtBegin", date);
            parameters.Add("startingDtEnd", date);
            return await search(parameters);
        }
	}
}
