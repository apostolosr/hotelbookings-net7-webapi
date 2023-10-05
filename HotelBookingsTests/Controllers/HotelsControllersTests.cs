using Moq;
using HotelBookings.Controllers;
using HotelBookings.Services.Hotels;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingsTests.Controllers;

public class HotelsControllerTests
{
    [Fact]
    public void TestGetBySearchTermSuccessful()
    {
        // Arrange
        var mockHotelsService = new Mock<IHotelsService>();
        mockHotelsService.Setup(p => p.GetHotelsAsync(It.IsAny<string>())).ReturnsAsync(new List<Hotel> { MockHelper.GetMockHotel() } );

        var hotelsController = new HotelsController(mockHotelsService.Object);

        // Act
        var result = hotelsController.GetBySearchTermAsync("test");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var hotelsList = okObjectResult.Value as List<Hotel>;

        Assert.Equal(200, okObjectResult.StatusCode);
        Assert.Equal(1, hotelsList?.Count);
    }

    [Fact]
    public void TestGetBySearchTermSuccessfulButEmptyList()
    {
        // Arrange
        var mockHotelsService = new Mock<IHotelsService>();
        mockHotelsService.Setup(p => p.GetHotelsAsync(It.IsAny<string>())).ReturnsAsync(new List<Hotel>());

        var hotelsController = new HotelsController(mockHotelsService.Object);

        // Act
        var result = hotelsController.GetBySearchTermAsync("test");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var hotelsList = okObjectResult.Value as List<Hotel>;

        Assert.Equal(200, okObjectResult.StatusCode);
        Assert.Equal(0, hotelsList?.Count);
    }

    [Fact]
    public void TestCreateHotelSuccessful()
    {
        // Arrange
        var mockHotelsService = new Mock<IHotelsService>();
        mockHotelsService.Setup(p => p.CreateHotelAsync(It.IsAny<CreateHotelModel>())).ReturnsAsync( MockHelper.GetMockHotel() );

        var hotelsController = new HotelsController(mockHotelsService.Object);

        // Act
        var result = hotelsController.CreateHotelAsync( MockHelper.GetMockHotelRequestModel() );

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);

        var okObjectResult = (OkObjectResult)result.Result;
        var hotel = okObjectResult.Value as Hotel;

        Assert.Equal(okObjectResult.StatusCode, 200);
        Assert.Equal(MockHelper.HotelName, hotel?.Name);
    }
}
