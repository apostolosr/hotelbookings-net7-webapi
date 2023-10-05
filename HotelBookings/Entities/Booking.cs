namespace HotelBookings.Entities;

/// <summary>
/// The Booking entity
/// </summary>
public class Booking
{
    /// <summary>
    /// The booking ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The hotel ID the booking is for
    /// </summary>
    public int HotelId { get; set; }

    /// <summary>
    /// The customer's name
    /// </summary>
    public required string CustomerName { get; set; }

    /// <summary>
    /// The number of PAX (people)
    /// </summary>
    public int Pax { get; set; }
}

