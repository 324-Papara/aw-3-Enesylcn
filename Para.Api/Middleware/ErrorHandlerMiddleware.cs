using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Para.Api.Middleware;


public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;
     private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        this.next = next;
        _logger = logger;
        
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
            //You can log all requests processed at the middleware layer.
            _logger.LogInformation($"Incoming Request: {context.Request.Protocol} {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
        }
        catch (Exception ex)
        {
            //You can log all requests processed at the HandleException.
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      //Here you can log all exceptions at the middleware layer.
        _logger.LogInformation(exception.Message + "\n" + exception.StackTrace);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware.",
            Detailed = exception.Message
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
    
}