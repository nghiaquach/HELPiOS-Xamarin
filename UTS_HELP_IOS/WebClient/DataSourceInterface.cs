using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace HELPiOS
{
	public interface DataSourceInterface
	{
		Task<Response<T>> fetch<T>(string path, string parameters, HttpContent formData, Action action);
	}
}

