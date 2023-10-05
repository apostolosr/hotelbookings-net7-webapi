using AutoMapper;
using HotelBookings.Entities;
using HotelBookings.Models.Hotels;
namespace HotelBookings;

/// <summary>
/// An auto mapper for the Hotel model/entity
/// </summary>
public class HotelAutoMapperProfile : Profile
{
	public HotelAutoMapperProfile()
	{
        CreateMap<CreateHotelModel, Hotel>();
    }
}

