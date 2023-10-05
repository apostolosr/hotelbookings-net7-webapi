using AutoMapper;
using HotelBookings.Entities;
using HotelBookings.Models.Bookings;
namespace HotelBookings;

/// <summary>
/// An auto mapper for the Booking model/entity
/// </summary>
public class BookingAutoMapperProfile : Profile
{
	public BookingAutoMapperProfile()
	{
        CreateMap<CreateBookingModel, Booking>();
        CreateMap<UpdateBookingModel, Booking>();
    }
}

