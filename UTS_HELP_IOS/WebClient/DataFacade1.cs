using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPiOS
{

    public enum Action { Get, Post, Put, Delete };

    public class DataFacade
    {
        private async Task<Response<T>> fetch<T>(string path, Object parameters, Action action)
        {
            DataSourceInterface db;
            // STUB
            // if (we have interwebs at request time)
            db = new RestClient();
            // else if (get, not post) AND (request is cache-compatible)
            //   db = new WorkshopCache();
            // else
            //   throw some no interwebs exception
            return await db.fetch<T>(path, JsonConvert.SerializeObject(parameters), action);
        }

        public async Task<Response<T>> get<T>(string path, Object parameters)
        {
            return await fetch<T>(path, parameters, Action.Get);
        }

        public async Task<Response<T>> post<T>(string path, Object parameters)
        {
            return await fetch<T>(path, parameters, Action.Post);
        }

        public async Task<Response<T>> put<T>(string path, Object parameters)
        {
            return await fetch<T>(path, parameters, Action.Put);
        }

        public async Task<Response<T>> delete<T>(string path, Object parameters)
        {
            return await fetch<T>(path, parameters, Action.Delete);
        }

    }
}