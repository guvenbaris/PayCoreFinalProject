using Newtonsoft.Json;
using PayCore.Application.Exceptions;
using System.Net;

namespace Paycore.WebAPI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await (HandleException(context, ex));
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            switch (ex)
            {
                case CustomException e:
                    response.StatusCode = (int)e.HttpStatusCode;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonConvert.SerializeObject(new { message = ex?.Message });
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomeExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
