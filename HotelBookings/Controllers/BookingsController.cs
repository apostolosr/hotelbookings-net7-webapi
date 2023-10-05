using Microsoft.AspNetCore.Mvc;
using HotelBookings.Services.Bookings;
using HotelBookings.Models.Bookings;

namespace HotelBookings.Controllers;

/// <summary>
/// The Bookings controller
/// </summary>
[ApiController]
[Route(Routes.Bookings)]
public class BookingsController : ControllerBase
{
    private readonly IBookingsService _bookingsService;

    /// <summary>
    /// The Bookings controller constructor
    /// </summary>
    /// <param name="bookingsService">The Bookings service</param>
    public BookingsController(IBookingsService bookingsService)
    {
        _bookingsService = bookingsService;
    }

    /// <summary>
    /// Method for getting bookings given the hotel ID
    /// </summary>
    /// <param name="hotel">The hotel ID</param>
    /// <returns>Response with array of bookings</returns>
    [HttpGet(Name = "GetBookings")]
    public async Task<IActionResult> GetBookingsByHotelAsync(int hotelId)
    {
        var bookings = await _bookingsService.GetBookingsAsync(hotelId).ConfigureAwait(false);
        return Ok(bookings);
    }

    /// <summary>
    /// Method for creating a booking given name, address and rating
    /// </summary>
    /// <param name="request">The create request model</param>
    /// <returns>Response with created booking</returns>
    [HttpPost(Name = "CreateBooking")]
    public async Task<IActionResult> CreateBookingAsync(CreateBookingModel request)
    {
        var bookingCreated = await _bookingsService.CreateBookingAsync(request).ConfigureAwait(false);
        return Ok(bookingCreated);
    }

    /// <summary>
    /// Method for updating a booking given its ID and values for the customer name and Pax number
    /// </summary>
    /// <param name="request">The update request model</param>
    /// <returns>Response with updated booking</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBookingAsync(int id, UpdateBookingModel request)
    {
        var bookingCreated = await _bookingsService.UpdateBookingAsync(id, request).ConfigureAwait(false);
        return Ok(bookingCreated);
    }
}
