using Moq;
using HotelBookings.Database;
using HotelBookings.Services.Hotels;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HotelBookings;
using AutoMapper;

namespace HotelBookingsTests.Services;

public class HotelServiceTests
{
    [Fact]
    public async void TestGeHotelsAsync()
    {
        // Arrange 
        var mockDataContext = new DataContext(new Mock<IConfiguration>().Object)
        {
            Hotels = MockHelper.GetQueryableMockDbSet( MockHelper.GetMockHotel()).Object
        };

        var hotelsService = new HotelsService(mockDataContext, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new HotelAutoMapperProfile()))));

        // Act
        List<Hotel> result = (List<Hotel>)await hotelsService.GetHotelsAsync(MockHelper.HotelName).ConfigureAwait(false);

        // Assert
        Assert.Equal(1, result?.Count);
        Assert.Equal(MockHelper.HotelId, result[0].Id);

        // Act, testing case-insensitivity
        result = (List<Hotel>)await hotelsService.GetHotelsAsync(MockHelper.HotelName.ToUpper()).ConfigureAwait(false);

        // Assert
        Assert.Equal(1, result?.Count);
        Assert.Equal(MockHelper.HotelId, result[0].Id);
    }

    [Fact]
    public async void TestCreateHotelAsync()
    {
        // Arrange
        var mockHotel = MockHelper.GetMockHotel();
        var mockDataContext = new Mock<DataContext>(new Mock<IConfiguration>().Object);
        mockDataContext.Setup(p => p.SaveChanges()).Returns(1);

        var internalEntityEntry = new InternalEntityEntry(
                new Mock<IStateManager>().Object,
                new RuntimeEntityType("T", typeof(Hotel), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false, null),
                mockHotel);

        var mockEntityEntry = new EntityEntry<Hotel>(internalEntityEntry);

        var mockDbSet = MockHelper.GetQueryableMockDbSet(mockHotel);
        mockDbSet.Setup(d => d.Add(It.IsAny<Hotel>())).Returns(mockEntityEntry);
        mockDataContext.Object.Hotels = mockDbSet.Object;

        var hotelsService = new HotelsService(mockDataContext.Object, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new HotelAutoMapperProfile()))));
        var mockHotelRequestModel = MockHelper.GetMockHotelRequestModel();
        mockHotelRequestModel.Address = "Neverland";

        // Act
        Hotel result = await hotelsService.CreateHotelAsync(mockHotelRequestModel).ConfigureAwait(false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockHotel.Id, result.Id);
        Assert.Equal(mockHotel.Name, result.Name);
        Assert.Equal(mockHotel.Address, result.Address);
        Assert.Equal(mockHotel.StarRating, result.StarRating);

    }

    [Fact]
    public void TestCreateHotelAsyncAddressExists()
    {
        // Arrange
        var mockDataContext = new Mock<DataContext>(new Mock<IConfiguration>().Object);
        mockDataContext.Setup(p => p.SaveChanges()).Returns(1);

        var internalEntityEntry = new InternalEntityEntry(
                new Mock<IStateManager>().Object,
                new RuntimeEntityType("T", typeof(Hotel), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false, null),
                MockHelper.GetMockHotel());

        var mockEntityEntry = new EntityEntry<Hotel>(internalEntityEntry);

        var mockDbSet = MockHelper.GetQueryableMockDbSet(MockHelper.GetMockHotel());
        mockDbSet.Setup(d => d.Add(It.IsAny<Hotel>())).Returns(mockEntityEntry);
        mockDataContext.Object.Hotels = mockDbSet.Object;

        var hotelsService = new HotelsService(mockDataContext.Object, new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new HotelAutoMapperProfile()))));
        var mockHotelRequestModel = MockHelper.GetMockHotelRequestModel();

        // Act, Assert
        Assert.ThrowsAsync<ApiException>(async () => await hotelsService.CreateHotelAsync(mockHotelRequestModel).ConfigureAwait(false));
    }
}
