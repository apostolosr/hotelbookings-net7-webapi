using Moq;
using HotelBookings.Controllers;
using HotelBookings.Services.Bookings;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingsTests.Controllers;

public class BookingsControllerTests
{
    [Fact]
    public void TestGetBookingsByHotelSuccessful()
    {
        // Arrange
        var mockBookingsService = new Mock<IBookingsService>();
        mockBookingsService.Setup(p => p.GetBookingsAsync(It.IsAny<int>())).ReturnsAsync(new List<Booking> { MockHelper.GetMockBooking() });

        var bookingsController = new BookingsController(mockBookingsService.Object);

        // Act
        var result = bookingsController.GetBookingsByHotelAsync(MockHelper.HotelId);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var bookingsList = okObjectResult.Value as List<Booking>;

        Assert.Equal(200, okObjectResult.StatusCode);
        Assert.Equal(1, bookingsList?.Count);
    }

    [Fact]
    public void TestGetBookingsByHotelSuccessfulButEmptyList()
    {
        // Arrange
        var mockBookingsService = new Mock<IBookingsService>();
        mockBookingsService.Setup(p => p.GetBookingsAsync(It.IsAny<int>())).ReturnsAsync(new List<Booking>());

        var bookingsController = new BookingsController(mockBookingsService.Object);

        // Act
        var result = bookingsController.GetBookingsByHotelAsync(MockHelper.HotelId);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var bookingsList = okObjectResult.Value as List<Booking>;

        Assert.Equal(200, okObjectResult.StatusCode);
        Assert.Equal(0, bookingsList?.Count);
    }

    [Fact]
    public void TestCreateBookingSuccessful()
    {
        // Arrange
        var mockBookingsService = new Mock<IBookingsService>();
        mockBookingsService.Setup(p => p.CreateBookingAsync(It.IsAny<CreateBookingModel>())).ReturnsAsync(MockHelper.GetMockBooking());

        var bookingsController = new BookingsController(mockBookingsService.Object);

        // Act
        var result = bookingsController.CreateBookingAsync(MockHelper.GetMockCreateBookRequestModel());

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var booking = okObjectResult.Value as Booking;

        Assert.Equal(okObjectResult.StatusCode, 200);
        Assert.Equal(MockHelper.HotelId, booking?.HotelId);
    }

    [Fact]
    public void TestUpdateBookingSuccessful()
    {
        // Arrange
        var mockBookingsService = new Mock<IBookingsService>();
        mockBookingsService.Setup(p => p.UpdateBookingAsync(It.IsAny<int>(), It.IsAny<UpdateBookingModel>())).ReturnsAsync(MockHelper.GetMockBooking());

        var bookingsController = new BookingsController(mockBookingsService.Object);

        // Act
        var result = bookingsController.UpdateBookingAsync(MockHelper.BookingId, MockHelper.GetMockUpdateBookRequestModel());

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var booking = okObjectResult.Value as Booking;

        Assert.Equal(okObjectResult.StatusCode, 200);
        Assert.Equal(MockHelper.HotelId, booking?.HotelId);
        Assert.Equal(MockHelper.BookingId, booking?.Id);
    }
}
