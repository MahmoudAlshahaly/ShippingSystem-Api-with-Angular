using System.Collections.Generic;
namespace Shipping.API.middelware
{


    public class SampleMiddleware

    {

        private readonly RequestDelegate _next;

        private readonly ILogger<SampleMiddleware> _logger;

        public SampleMiddleware(RequestDelegate next, ILogger<SampleMiddleware> logger)

        {

            _next = next ?? throw new ArgumentNullException(nameof(next));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task Invoke(HttpContext httpContext)
        {

            _logger.LogInformation("Before Sample Middleware");

            var b = httpContext.Response.StatusCode;

            await _next(httpContext);

            var a=   httpContext.Response.StatusCode;


            _logger.LogInformation("After Sample Middleware");

        }

    }

}
