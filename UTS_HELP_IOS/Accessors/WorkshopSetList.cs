using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UTS_HELP_ANDROID.Resources.WebClient;
using System.Threading.Tasks;
using UTS_HELP_ANDROID.Resources.Models;
using UTS_HELP_ANDROID.Resources.Exceptions;

namespace UTS_HELP_ANDROID.Resources.Accessors
{
    public class WorkshopSetList
    {
        private const string apiUri = "workshop/workshopSets/";
        private DataFacade db;

        public WorkshopSetList()
        {
            db = new DataFacade();
        }

        public async Task<List<WorkshopSet>> listAll()
        {
            Response<WorkshopSet> response = await db.get<WorkshopSet>(apiUri + "/active", null);
            if (response.IsSuccess && response.Results != null)
            {
                return response.Results;
            }
            else
            {
                throw new WebserviceFailureException(response.DisplayMessage);
            }
        }
    }
}