
namespace HotelBookings;

/// <summary>
/// Custom api exception
/// </summary>
public class ApiException : Exception
{
	public ApiException() : base() { }
	public ApiException(string message) : base(message) { }
}

