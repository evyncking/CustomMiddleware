using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();

app.MapGet("/", () => "Authorized");

app.Run();

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var username = context.Request.Query["username"];
        var password = context.Request.Query["password"];

        if (username != "user1" || password != "password1")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Not authorized.");
            return;
        }

        await _next(context);
    }
}