using System;
using System.Collections.Generic;

namespace HELPiOS
{
	public class Response<T>
	{
		public bool IsSuccess { get; set; }

		public string DisplayMessage { get; set; }

		public List<T> Results { get; set; }
	}
}

