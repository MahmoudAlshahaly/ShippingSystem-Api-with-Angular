using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Shipping.BLL.Managers.Block;
using System.Globalization;
using System.Net;
using System.Runtime;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace Shipping.API.Filters
{
    public class IpBlockActionFilterAttribute: ActionFilterAttribute
    {
        private readonly IIpBlockingService _blockingService;
        public IpBlockActionFilterAttribute(IIpBlockingService blockingService)
        {
            _blockingService = blockingService;
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            var a=   GetClientMAC();
            //var counr=  GetUserCountryByIp(remoteIp.ToString());
            var isBlocked = _blockingService.IsBlocked(remoteIp);
            //var isBlocked = _blockingService.IsBlocked(IPAddress.Parse("85.13.150.143"));
            if (isBlocked)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
        public  string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo.Country;
        }
        public class IpInfo
        {
            public string Country { get; set; }
        }
        private  string GetClientMAC()
        {
            NetworkInterface[] nic = NetworkInterface.GetAllNetworkInterfaces();
            string sMacAddr = string.Empty;
            foreach (NetworkInterface adapter in nic)
            {
                if (sMacAddr == string.Empty)//only returns MAC Address from first card
                {
                    IPInterfaceProperties prop = adapter.GetIPProperties();
                    sMacAddr = adapter.GetPhysicalAddress().ToString();
                }

            }
            return sMacAddr;
        }

    }
}
