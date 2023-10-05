using HotelBookings.Models.Hotels;
using HotelBookings.Entities;
namespace HotelBookings.Services.Hotels;

/// <summary>
/// The Hotels service interface
/// </summary>
public interface IHotelsService
{
    /// <summary>
    /// Method for getting the hotels by name, given a substring (case-insensitive)
    /// </summary>
    /// <param name="term">The search term (substring)</param>
    /// <returns>An enumerable with the hotels</returns>
    Task<IEnumerable<Hotel>> GetHotelsAsync(string term);
    /// <summary>
    /// Method for creating a new hotel
    /// </summary>
    /// <param name="request">The create request model</param>
    /// <returns>The created hotel entity</returns>
    Task<Hotel> CreateHotelAsync(CreateHotelModel request);
} 

