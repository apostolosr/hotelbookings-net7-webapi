using HotelBookings.Database;
using HotelBookings.Models.Hotels;
using HotelBookings.Entities;
using AutoMapper;
namespace HotelBookings.Services.Hotels;

/// <summary>
/// The Hotels service
/// </summary>
public class HotelsService : IHotelsService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// The Hotels service constructor
    /// </summary>
    /// <param name="context">The data context</param>
    /// <param name="mapper">The auto mapper</param>
	public HotelsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<inheritdoc>
    public async Task<Hotel> CreateHotelAsync(CreateHotelModel request)
    {
        return await Task.Run(() =>
        {
            if (_context.Hotels.Any(x => x.Address == request.Address))
                throw new ApiException($"Hotel with address {request.Address} already exists");

            var hotel = _mapper.Map<Hotel>(request);

            var entityEntry = _context.Hotels.Add(hotel);
            _context.SaveChanges();
            return entityEntry.Entity;
        }).ConfigureAwait(false);
    }

    ///<inheritdoc>
    public async Task<IEnumerable<Hotel>> GetHotelsAsync(string term)
    {
        return await Task.Run(() =>
        {
            return _context.Hotels.ToList().Where(x => x.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }).ConfigureAwait(false);
    }
}
 
