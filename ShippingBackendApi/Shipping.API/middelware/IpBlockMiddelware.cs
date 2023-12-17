using Shipping.BLL.Managers.Block;
using System.Net;

namespace Shipping.API.middelware
{
    public class IpBlockMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly IIpBlockingService _blockingService;
        public IpBlockMiddelware(RequestDelegate next, IIpBlockingService blockingService)
        {
            _next = next;
            _blockingService = blockingService;
        }
        public async Task Invoke(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress;
            var isBlocked = _blockingService.IsBlocked(remoteIp!);
            if (isBlocked)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _next.Invoke(context);
        }
    }
}
