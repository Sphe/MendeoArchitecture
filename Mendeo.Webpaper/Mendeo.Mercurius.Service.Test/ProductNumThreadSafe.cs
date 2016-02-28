using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Test
{
    public class ProductNumThreadSafe
    {
        private long _numProduct = 0;

        public long GetNextNumber()
        {
            return Interlocked.Increment(ref _numProduct);
        }
    }
}
