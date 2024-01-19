namespace Shipping.API.middelware
{
    public class TestMiddelware
    {
        private readonly ILogger<TestMiddelware> _logger;
        private readonly RequestDelegate _next;

        public TestMiddelware(ILogger<TestMiddelware> logger,RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("before isolated middelware 2");
            int s = httpContext.Response.StatusCode;
             await _next(httpContext);
            _logger.LogInformation("after isolated middelware 2");
        }
    }
}
