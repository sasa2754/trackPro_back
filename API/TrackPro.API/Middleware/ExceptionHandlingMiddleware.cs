using System.Net;
using System.Text.Json;
using TrackPro.Application.Exceptions;

namespace TrackPro.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            if (exception is ApiException apiException)
            {
                statusCode = apiException.StatusCode;
                message = apiException.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "An unexpected internal server error has occurred.";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new { error = message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}