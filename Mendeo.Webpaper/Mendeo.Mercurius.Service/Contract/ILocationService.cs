using Mendeo.Mercurius.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface ILocationService
    {
        Task<IpGeoLocalisationDto> GetGeoLocalisationFromIp(string ip);
    }
}
