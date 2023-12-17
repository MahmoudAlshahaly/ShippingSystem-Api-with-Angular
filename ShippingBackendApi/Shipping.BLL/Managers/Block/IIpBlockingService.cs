using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers.Block
{
    public interface IIpBlockingService
    {
        bool IsBlocked(IPAddress ipAddress);
    }
    public class IpBlockingService : IIpBlockingService
    {
        private readonly List<string> _blockedIps;
        public IpBlockingService(IConfiguration configuration)
        {
            var blockedIps = configuration.GetSection("BlockedIPs").Value;
            _blockedIps = blockedIps.Split(',').ToList();
        }
        public bool IsBlocked(IPAddress ipAddress) => _blockedIps.Contains(ipAddress.ToString());
    }
}
