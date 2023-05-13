using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using log4net;

namespace UserAPI.Middleware;
public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILog logger;

    public LoggingMiddleware(RequestDelegate next, ILog logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        logger.Info($"Request {context.Request.Method} {context.Request.Path}");

        await next(context);

        logger.Info($"Response {context.Response.StatusCode}");
    }
}
