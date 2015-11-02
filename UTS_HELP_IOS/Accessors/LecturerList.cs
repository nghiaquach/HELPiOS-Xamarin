using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HELPiOS
{
    public class LecturerList
    {
        private const string apiUri = "misc/lecturer/";
        private DataFacade db;

    	public LecturerList()
	    {
            db = new DataFacade();
	    }

        public async Task<Lecturer> getById(int id)
        {
            Response<Lecturer> response = await db.get<Lecturer>(apiUri + "/true", null);
            if (response.IsSuccess)
            {
                foreach (Lecturer lecturer in response.Results)
                {
                    if (lecturer.id == id)
                        return lecturer;
                }
                throw new LecturerNotFound(response.DisplayMessage);
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }

        public async Task<HashSet<Lecturer>> filterByName(string searchTerm)
        {
            Response<Lecturer> response = await db.get<Lecturer>(apiUri + "/true", null);
            if (response.IsSuccess)
            {
                HashSet<Lecturer> lecturers = new HashSet<Lecturer>();
                foreach (Lecturer lecturer in response.Results)
                {
					if ((lecturer.first_name + " " + lecturer.last_name).Contains (searchTerm))
						Console.WriteLine ("lecturer .....");
						lecturers.Add(lecturer);
                }
                return lecturers;
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }
    }
}