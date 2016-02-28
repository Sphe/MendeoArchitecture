using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service
{
    public class CultureService : ICultureService
    {
        private Culture _current;

        public CultureService()
        {
            _current = new Culture();
        }

        public int CurrentCultureId
        {
            get
            {
                return _current.CultureID;
            }
        }

        public Culture CurrentCulture
        {
            get
            {
                return _current;
            }
        }
    }
}
