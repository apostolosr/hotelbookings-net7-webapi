namespace HotelBookings.Entities;

/// <summary>
/// The Hotel entity
/// </summary>
public class Hotel
{
    /// <summary>
    /// The hotel ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the hotel
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The address of the hotel
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// THe hotel's rating (1-5 stars)
    /// </summary>
    public int StarRating { get; set; }
}

