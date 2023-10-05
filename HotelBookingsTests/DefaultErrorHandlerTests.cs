using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Moq;

namespace HotelBookingsTests
{
	public class DefaultErrorHandlerTests
	{
        [Fact]
        public async void TestInvokeAsyncApiException()
        {
            // Arrange 
            HttpContext ctx = new DefaultHttpContext();

            RequestDelegate next = (HttpContext hc) => throw new ApiException();
            var defaultErrorHandler = new DefaultErrorHandler(next, new Mock<ILogger<DefaultErrorHandler>>().Object);

            // Act
            await defaultErrorHandler.InvokeAsync(ctx).ConfigureAwait(false);


            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, ctx.Response.StatusCode);
        }

        [Fact]
        public async void TestInvokeAsyncKeyNotFoundException()
        {
            // Arrange 
            HttpContext ctx = new DefaultHttpContext();

            RequestDelegate next = (HttpContext hc) => throw new KeyNotFoundException();
            var defaultErrorHandler = new DefaultErrorHandler(next, new Mock<ILogger<DefaultErrorHandler>>().Object);

            // Act
            await defaultErrorHandler.InvokeAsync(ctx).ConfigureAwait(false);


            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, ctx.Response.StatusCode);
        }

        [Fact]
        public async void TestInvokeAsyncDefaultException()
        {
            // Arrange 
            HttpContext ctx = new DefaultHttpContext();

            RequestDelegate next = (HttpContext hc) => throw new Exception();
            var defaultErrorHandler = new DefaultErrorHandler(next, new Mock<ILogger<DefaultErrorHandler>>().Object);

            // Act
            await defaultErrorHandler.InvokeAsync(ctx).ConfigureAwait(false);


            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, ctx.Response.StatusCode);
        }
    }
}

