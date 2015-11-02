using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPiOS
{
    public class CampusList
    {
        private const string apiUri = "misc/campus/";
        private DataFacade db;

    	public CampusList()
	    {
            db = new DataFacade();
	    }

        public async Task<Campus> getById(int id)
        {
            Response<Campus> response = await db.get<Campus>(apiUri + "/true", null);
            if (response.IsSuccess)
            {
                foreach (Campus campus in response.Results)
                {
                    if (campus.id == id)
                        return campus;
                }
                throw new CampusNotFound(response.DisplayMessage);
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }

        public async Task<HashSet<Campus>> filterByName(string searchTerm)
        {
            Response<Campus> response = await db.get<Campus>(apiUri + "/true", null);
            if (response.IsSuccess)
            {
                HashSet<Campus> campuses = new HashSet<Campus>();

                foreach (Campus campus in response.Results)
                {
					if (campus.campus.Contains (searchTerm)) {
						Console.WriteLine ("search term ..... " + searchTerm );
						Console.WriteLine ("Campus ..... " + campus.campus);
						campuses.Add (campus);
					}
                }
                return campuses;
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }
    }
}