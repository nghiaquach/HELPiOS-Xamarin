using System;

namespace HELPiOS
{
    public abstract class HelpsBaseException : Exception
	{
		public HelpsBaseException(string message) : base(message) { }
		public HelpsBaseException(string message, Exception innerException) : base(message, innerException) { }
	}

    public class WebserviceFailureException : HelpsBaseException
    {
        public WebserviceFailureException(string message) : base(message) { }
        public WebserviceFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class StudentNotFoundException : HelpsBaseException
	{
		public StudentNotFoundException(string message) : base(message) { }
		public StudentNotFoundException(string message, Exception innerException) : base(message, innerException) { }
	}

    public class StudentNotRegistered : HelpsBaseException
	{
		public StudentNotRegistered(string message) : base(message) { }
		public StudentNotRegistered(string message, Exception innerException) : base(message, innerException) { }
	}

    public class WorkshopNotFound : HelpsBaseException
    {
        public WorkshopNotFound(string message) : base(message) { }
        public WorkshopNotFound(string message, Exception innerException) : base(message, innerException) { }
    }
}
