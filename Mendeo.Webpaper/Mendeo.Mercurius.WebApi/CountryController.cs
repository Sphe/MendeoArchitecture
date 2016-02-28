using Mendeo.Common.WebApi;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Dto.ProductDetail;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mendeo.Mercurius.WebApi
{
    public class CountryController : ApiController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public ProductDetailCountryDto GetCountryByISO2(string iso2)
        {
            return _countryService.GetCountryByCodeISO2(iso2);
        }
    }
}
