using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace e_store_be.Middlewares;

//creates a middleware that handles exceptions in the api
public class ExceptionMiddleware(IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //creates a response object that will be sent back from the api
        var response = new ProblemDetails
        {
            Status = 500,
            Detail = env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
            Title = ex.Message,
        };

        var options = new JsonSerializerOptions
        {
            //sets correct camel casing for json
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        //serializes our response to be a json object for our front-end
        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
