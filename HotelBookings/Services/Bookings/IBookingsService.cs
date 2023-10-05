using HotelBookings.Entities;
using HotelBookings.Models.Bookings;

namespace HotelBookings.Services.Bookings;

/// <summary>
/// The Bookings service interface
/// </summary>
public interface IBookingsService
{
    /// <summary>
    /// Method for getting bookings given the hotel ID they are for
    /// </summary>
    /// <param name="hotelId">The hotel ID</param>
    /// <returns>An enumerable woth the bookings</returns>
    Task<IEnumerable<Booking>> GetBookingsAsync(int hotelId);

    /// <summary>
    /// Method for creating a booking given hotel ID, customer name and Pax number
    /// </summary>
    /// <param name="request">The create request model</param>
    /// <returns>The created booking entity</returns>
    Task<Booking> CreateBookingAsync(CreateBookingModel request);

    // <summary>
    /// Method for updating a booking's customer name and Pax number
    /// </summary>
    /// <param name="request">The update request model</param>
    /// <returns>The updated booking entity</returns>
    Task<Booking> UpdateBookingAsync(int id, UpdateBookingModel request);
}

