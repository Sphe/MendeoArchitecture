using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace IRL.Controllers
{
    public static class MapperExtensions
    {
        public static D GenericMapper<S, D>(this S source)
        {
            return Mapper.Map<S, D>(source);
        }
    }
}
