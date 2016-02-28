using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Mendeo.Mercurius.WebApi
{
    public class LocationController : ApiController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IpGeoLocalisationDto> GetIpLocationInfo()
        {
            var ip = GetClientIp();
            return await _locationService.GetGeoLocalisationFromIp(ip);
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            
            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                var prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            
            return HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : null;
        }
    }
}
