using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HELPiOS
{
    public class StudentList
	{
		private const string apiUri = "student/";
		private DataFacade db;

		public StudentList()
		{
			db = new DataFacade();
		}

        public async Task<bool> login(string studentId, string password)
        {
            Dictionary<string, Object> formData = new Dictionary<string, Object>();
            formData.Add("studentId", studentId);
            formData.Add("password", password);
            Response<Object> response = await db.post<Object>(apiUri + "login", null, formData);
            return response.IsSuccess;
        }

		public async Task<Student> getById(string studentId)
		{
            Dictionary<string, Object> parameters = new Dictionary<string, Object>();
            parameters.Add("studentID", studentId);
            Response<Student> response = await db.get<Student>(apiUri + "search", parameters);
            if (response.IsSuccess && response.Results != null && response.Results.Count == 1)
				return response.Results[0];
			else
				throw new StudentNotFoundException(response.DisplayMessage); /* Crashing here?
            *  Our database might have multiple records with the same student id. Only one should
            *  ever be returned, the non-archived row; the others are safe to delete. See
            *  https://online.uts.edu.au/webapps/discussionboard/do/message?action=list_messages&forum_id=_237578_1&nav=discussion_board_entry&conf_id=_80135_1&course_id=_21259_1&message_id=_2944746_1#msg__2944746_1Id
            */
		}

        public async Task create(Student student)
		{
            Response<Object> response = await db.post<Object>(apiUri + "register", null, student);
			if (!response.IsSuccess)
				throw new StudentNotRegistered(response.DisplayMessage);
		}

        public async Task update(Student student)
        {
            Response<Object> response = await db.put<Object>(apiUri + "update", null, student);
            if (!response.IsSuccess)
                throw new StudentNotUpdated(response.DisplayMessage);
        }
	}
}
