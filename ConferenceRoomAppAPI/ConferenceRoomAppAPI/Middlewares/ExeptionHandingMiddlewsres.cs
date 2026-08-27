using System.Net;
using ConferenceRoomAppAPI.Services.Exceptions;
using System.Text.Json;

namespace ConferenceRoomAppAPI.Middlewares
{
    public class ExeptionHandingMiddlewsres
    {
        private readonly RequestDelegate _next;

        public ExeptionHandingMiddlewsres(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ConferenceRoomBaseExeption ex)
            {
                context.Response.StatusCode = (int)ex.GetStatusCode();
                var response = new { error = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { error = "An unexpected error occurred." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
