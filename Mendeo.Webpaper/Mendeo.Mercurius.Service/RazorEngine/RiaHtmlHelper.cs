using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Text;

namespace Mendeo.Mercurius.Service.RazorEngine
{
    public class RiaHtmlHelper
    {
        public IEncodedString Raw(string rawString)
        {
            return new RawString(rawString);
        }

        public T First<T>(ICollection<T> collection)
        {
            return collection.First();
        }
    }
}
