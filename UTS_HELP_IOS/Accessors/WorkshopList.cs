using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
	class WorkshopList
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
            Response<AbstractWorkshop> response = await db.get<AbstractWorkshop>(apiUri + "/search", parameters);
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
	}
}
