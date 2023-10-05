using Microsoft.AspNetCore.Mvc;
using HotelBookings.Services.Hotels;
using HotelBookings.Models.Hotels;

namespace HotelBookings.Controllers;


/// <summary>
/// The Hotels controller
/// </summary>
[ApiController]
[Route(Routes.Hotels)]
public class HotelsController : ControllerBase
{
    private readonly IHotelsService _hotelsService;

    /// <summary>
    /// The Hotels controller constructor
    /// </summary>
    /// <param name="hotelsService">The Hotels service</param>
    public HotelsController(IHotelsService hotelsService)
    {
        _hotelsService = hotelsService;
    }

    /// <summary>
    /// Method for getting hotels by name, given a serach term
    /// </summary>
    /// <param name="term">The substring that the name must contain</param>
    /// <returns>Response with array of hotels</returns>
    [HttpGet(Name = "GetHotels")]
    public  async Task<IActionResult> GetBySearchTermAsync(string term)
    {
        var hotels = await _hotelsService.GetHotelsAsync(term).ConfigureAwait(false);
        return Ok(hotels);
    }

    /// <summary>
    /// Method for creating a hotel given name, address and rating
    /// </summary>
    /// <param name="request">The create request model</param>
    /// <returns>Response with created hotel</returns>
    [HttpPost(Name = "CreateHotel")]
    public async Task<IActionResult> CreateHotelAsync(CreateHotelModel request)
    {
        var hotelCreated = await _hotelsService.CreateHotelAsync(request).ConfigureAwait(false);
        return Ok(hotelCreated);
    }
}
