using HotelBookings.Database;
using HotelBookings.Entities;
using HotelBookings.Models.Bookings;
using AutoMapper;
namespace HotelBookings.Services.Bookings;

/// <summary>
/// The Bookings service
/// </summary>
public class BookingsService : IBookingsService
{
    private DataContext _context;
    private IMapper _mapper;

    /// <summary>
    /// The Bookings service constructor
    /// </summary>
    /// <param name="context">The data context</param>
    /// <param name="mapper">The auto mapper</param>
	public BookingsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<inheritdoc>
    public async Task<Booking> CreateBookingAsync(CreateBookingModel request)
    {
        return await Task.Run(() =>
        {
            if (!_context.Hotels.Any(x => x.Id == request.HotelId))
                throw new ApiException($"Hotel with ID {request.HotelId} does not exist");

            var booking = _mapper.Map<Booking>(request);

            var temp = _context.Bookings.Add(booking);
            _context.SaveChanges();
            return temp.Entity;
        }).ConfigureAwait(false);
    }

    ///<inheritdoc>
    public async Task<IEnumerable<Booking>> GetBookingsAsync(int hotelId)
    {
        return await Task.Run(() =>
        {
            if (!_context.Hotels.Any(x => x.Id == hotelId))
                throw new ApiException($"Hotel with ID {hotelId} does not exist");

           return _context.Bookings.Where(x => x.HotelId == hotelId).ToList();
        }).ConfigureAwait(false);
    }

    ///<inheritdoc>
    public async Task<Booking> UpdateBookingAsync(int id, UpdateBookingModel request)
    {
        return await Task.Run(() =>
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No booking found with Id {id}");

            _mapper.Map(request, booking);
            
            _context.SaveChanges();
            return booking;
        }).ConfigureAwait(false);
    }
}

