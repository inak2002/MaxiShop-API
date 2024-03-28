using MaxiShop.Application.Exceptions;
using MaxiShop.Web.Models;
using System.Net;

namespace MaxiShop.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpcontext, ex);    
            }

        }
        private async Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();
            switch (ex)
            {
                case BadRequestException badRequestExceptio:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails() 
                    { 
                        Title= badRequestExceptio.Message,
                        Status=(int)statusCode,
                        Type=nameof(BadRequestException),
                        Detail=badRequestExceptio.InnerException?.Message,
                        Errors= badRequestExceptio.ValidationsErrors
                    };
                break;
            }
            httpcontext.Response.StatusCode=(int)statusCode;
            await httpcontext.Response.WriteAsJsonAsync(problem);
        }
    }
}
