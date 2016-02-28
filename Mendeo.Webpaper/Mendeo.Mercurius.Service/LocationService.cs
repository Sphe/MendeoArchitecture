using Mendeo.Common.Core;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Service.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service
{
    public class LocationService : ILocationService
    {
        public async Task<IpGeoLocalisationDto> GetGeoLocalisationFromIp(string ip)
        {
            Check.Require(!string.IsNullOrWhiteSpace(ip), "ip must not be null or empty");

            var url = ConfigurationManager.AppSettings["IpLocationServiceUrl"];
            var key = ConfigurationManager.AppSettings["IpLocationServiceKey"];
            var query = ConfigurationManager.AppSettings["IpLocationServiceQuery"];
            var realQuery = string.Format(query, key, ip);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(new Uri(realQuery, UriKind.Relative));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<IpGeoLocalisationDto>(content));
            }
        }
    }
}
