using Moq;
using HotelBookings.Database;
using HotelBookings.Services.Bookings;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HotelBookings;
using AutoMapper;

namespace HotelBookingsTests.Services;

public class BookingsServiceTests
{
    [Fact]
    public async void TestGetBookingsAsync()
    {
        // Arrange
        var mockDataContext = new DataContext(new Mock<IConfiguration>().Object)
        {
            Hotels = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockHotel()).Object,
            Bookings = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockBooking()).Object
        };

        var bookingsService = new BookingsService(mockDataContext, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BookingAutoMapperProfile()))));

        // Act
        List<Booking> result = (List<Booking>)await bookingsService.GetBookingsAsync(MockHelper.HotelId).ConfigureAwait(false);

        // Assert
        Assert.Equal(1, result?.Count);
        Assert.Equal(MockHelper.BookingId, result[0].Id);
    }

    [Fact]
    public void TestGetBookingsAsyncHotelIdDoesNotExist()
    {
        // Arrange
        var mockDataContext = new DataContext(new Mock<IConfiguration>().Object)
        {
            Hotels = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockHotel()).Object,
            Bookings = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockBooking()).Object
        };

        var bookingsService = new BookingsService(mockDataContext, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BookingAutoMapperProfile()))));

        // Act, Assert
        Assert.ThrowsAsync<ApiException>(async () => await bookingsService.GetBookingsAsync(4).ConfigureAwait(false));
    }

    [Fact]
    public async void TestCreateBookingAsync()
    {
        // Arrange
        var mockBooking = MockHelper.GetMockBooking();
        var mockDataContext = new Mock<DataContext>(new Mock<IConfiguration>().Object);
        mockDataContext.Setup(p => p.SaveChanges()).Returns(1);

        var internalEntityEntry = new InternalEntityEntry(
                new Mock<IStateManager>().Object,
                new RuntimeEntityType("T", typeof(Hotel), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false, null),
                mockBooking);

        var mockEntityEntry = new EntityEntry<Booking>(internalEntityEntry);

        var mockDbSetBookings = MockHelper.GetQueryableMockDbSet(mockBooking);
        mockDbSetBookings.Setup(d => d.Add(It.IsAny<Booking>())).Returns(mockEntityEntry);
        var mockDbSetHotels = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockHotel());
        mockDataContext.Object.Bookings = mockDbSetBookings.Object;
        mockDataContext.Object.Hotels = mockDbSetHotels.Object;

        var bookingsService = new BookingsService(mockDataContext.Object, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BookingAutoMapperProfile()))));
        var mockCreateBookingRequestModel = MockHelper.GetMockCreateBookRequestModel();

        // Act
        Booking result = await bookingsService.CreateBookingAsync(mockCreateBookingRequestModel).ConfigureAwait(false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockBooking.Id, result.Id);
        Assert.Equal(mockBooking.CustomerName, result.CustomerName);
        Assert.Equal(mockBooking.HotelId, result.HotelId);
        Assert.Equal(mockBooking.Pax, result.Pax);
    }

    [Fact]
    public void TestCreateBookingAsyncHotelIdDoesNotExist()
    {
        // Arrange
        var mockBooking = MockHelper.GetMockBooking();
        var mockDataContext = new Mock<DataContext>(new Mock<IConfiguration>().Object);
        mockDataContext.Setup(p => p.SaveChanges()).Returns(1);

        var internalEntityEntry = new InternalEntityEntry(
                new Mock<IStateManager>().Object,
                new RuntimeEntityType("T", typeof(Hotel), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false, null),
                mockBooking);

        var mockEntityEntry = new EntityEntry<Booking>(internalEntityEntry);

        var mockDbSetBookings = MockHelper.GetQueryableMockDbSet(mockBooking);
        mockDbSetBookings.Setup(d => d.Add(It.IsAny<Booking>())).Returns(mockEntityEntry);
        var mockDbSetHotels = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockHotel());
        mockDataContext.Object.Bookings = mockDbSetBookings.Object;
        mockDataContext.Object.Hotels = mockDbSetHotels.Object;

        var bookingsService = new BookingsService(mockDataContext.Object, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BookingAutoMapperProfile()))));
        var mockCreateBookingRequestModel = MockHelper.GetMockCreateBookRequestModel();
        mockCreateBookingRequestModel.HotelId = 2;
        mockCreateBookingRequestModel.CustomerName = "John Wick";

        // Act, Assert
        Assert.ThrowsAsync<ApiException>(async () => await bookingsService.CreateBookingAsync(mockCreateBookingRequestModel).ConfigureAwait(false));
    }

    [Fact]
    public async void TestUpdateBookingAsync()
    {
        // Arrange
        var mockBooking = MockHelper.GetMockBooking();
        var mockDataContext = new Mock<DataContext>(new Mock<IConfiguration>().Object);
        mockDataContext.Setup(p => p.SaveChanges()).Returns(1);
     
        var mockDbSetBookings = MockHelper.GetQueryableMockDbSet(mockBooking);
        mockDataContext.Object.Bookings = mockDbSetBookings.Object;

        var bookingsService = new BookingsService(mockDataContext.Object, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BookingAutoMapperProfile()))));
        var mockUpdateBookingRequestModel = MockHelper.GetMockUpdateBookRequestModel();
        mockUpdateBookingRequestModel.CustomerName = "John Wick";
        mockUpdateBookingRequestModel.Pax = 9;

        // Act
        Booking result = await bookingsService.UpdateBookingAsync(MockHelper.BookingId, mockUpdateBookingRequestModel).ConfigureAwait(false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockBooking.Id, result.Id);
        Assert.Equal(mockBooking.HotelId, result.HotelId);
        Assert.Equal(mockUpdateBookingRequestModel.CustomerName, result.CustomerName);
        Assert.Equal(mockUpdateBookingRequestModel.Pax, result.Pax);
    }

    [Fact]
    public void TestUpdateBookingAsyncKeyNotFound()
    {
        // Arrange
        var mockBooking = MockHelper.GetMockBooking();
        var mockDataContext = new Mock<DataContext>(new Mock<IConfiguration>().Object);
        mockDataContext.Setup(p => p.SaveChanges()).Returns(1);

        var mockDbSetBookings = MockHelper.GetQueryableMockDbSet(mockBooking);
        mockDataContext.Object.Bookings = mockDbSetBookings.Object;

        var bookingsService = new BookingsService(mockDataContext.Object, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BookingAutoMapperProfile()))));
        var mockUpdateBookingRequestModel = MockHelper.GetMockUpdateBookRequestModel();
        mockUpdateBookingRequestModel.CustomerName = "John Wick";
        mockUpdateBookingRequestModel.Pax = 9;

        // Act, Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await bookingsService.UpdateBookingAsync(3, mockUpdateBookingRequestModel).ConfigureAwait(false));
    }
}
