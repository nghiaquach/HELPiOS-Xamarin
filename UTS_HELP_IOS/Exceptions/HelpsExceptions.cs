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

	public class StudentNotUpdated : HelpsBaseException
	{
		public StudentNotUpdated(string message) : base(message) { }
		public StudentNotUpdated(string message, Exception innerException) : base(message, innerException) { }
	}

	public class StudentNotBooked : HelpsBaseException
	{
		public StudentNotBooked(string message) : base(message) { }
		public StudentNotBooked(string message, Exception innerException) : base(message, innerException) { }
	}

	public class WorkshopNotFound : HelpsBaseException
	{
		public WorkshopNotFound(string message) : base(message) { }
		public WorkshopNotFound(string message, Exception innerException) : base(message, innerException) { }
	}

	public class UnknownWorkshopType : HelpsBaseException
	{
		public UnknownWorkshopType(string message) : base(message) { }
		public UnknownWorkshopType(string message, Exception innerException) : base(message, innerException) { }
	}

	public class BookingNotCancelled : HelpsBaseException
	{
		public BookingNotCancelled(string message) : base(message) { }
		public BookingNotCancelled(string message, Exception innerException) : base(message, innerException) { }
	}

	public class CampusNotFound : HelpsBaseException
	{
		public CampusNotFound(string message) : base(message) { }
		public CampusNotFound(string message, Exception innerException) : base(message, innerException) { }
	}

	public class LecturerNotFound : HelpsBaseException
	{
		public LecturerNotFound(string message) : base(message) { }
		public LecturerNotFound(string message, Exception innerException) : base(message, innerException) { }
	}
}
