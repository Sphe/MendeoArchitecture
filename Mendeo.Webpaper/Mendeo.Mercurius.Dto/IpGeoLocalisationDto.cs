using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto
{
    public class IpGeoLocalisationDto
    {
        public string StatusCode { get; set; }
	    public string StatusMessage { get; set; }
	    public string IpAddress { get; set; }
	    public string CountryCode { get; set; }
	    public string CountryName { get; set; }
	    public string Latitude { get; set; }
	    public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }
}
