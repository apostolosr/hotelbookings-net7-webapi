using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HotelBookingsTests
{
	internal static class MockHelper
	{
        internal const int HotelId = 1;
        internal const int BookingId = 1;
        internal const string HotelName = "California";
        internal const string CustomerName = "Tolis";
        internal const string Address = "Somewhere";
        internal const int StarRating = 5;
        internal const int Pax = 4;

        internal static Hotel GetMockHotel()
		{
			return new Hotel { Id = HotelId, Name = HotelName, Address = Address, StarRating = StarRating };
		}

        internal static CreateHotelModel GetMockHotelRequestModel()
        {
            return new CreateHotelModel { Name = HotelName, Address = Address, StarRating = StarRating };
        }

        internal static Booking GetMockBooking()
        {
            return new Booking {Id = BookingId, HotelId = HotelId, CustomerName = CustomerName, Pax = 4 };
        }

        internal static CreateBookingModel GetMockCreateBookRequestModel()
        {
            return new CreateBookingModel { CustomerName = CustomerName, HotelId = HotelId, Pax = Pax };
        }

        internal static UpdateBookingModel GetMockUpdateBookRequestModel()
        {
            return new UpdateBookingModel { CustomerName = CustomerName, Pax = Pax };
        }

        internal static Mock<DbSet<T>> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSet;
        }
    }
}

