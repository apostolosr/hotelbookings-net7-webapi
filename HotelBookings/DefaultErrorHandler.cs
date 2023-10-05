namespace HotelBookings;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

/// <summary>
/// Error handling middleware
/// </summary>
public class DefaultErrorHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public DefaultErrorHandler(RequestDelegate next, ILogger<DefaultErrorHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (ex)
            {
                case ApiException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    _logger.LogError(ex, ex.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await response.WriteAsync(JsonSerializer.Serialize(new { message = ex?.Message })).ConfigureAwait(false);
        }
    }
}