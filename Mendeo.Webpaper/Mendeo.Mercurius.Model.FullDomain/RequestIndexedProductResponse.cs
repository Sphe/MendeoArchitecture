using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class RequestIndexedProductResponse
    {
        public long Total { get; set; }

        public long TotalBucketTree { get; set; }

        public long ContextTotal { get; set; }

        public int TimeRequestElapsed { get; set; }

        public string QuerySearch { get; set; }

        public IList<IndexedProduct> ContextProducts { get; set; }

        public IList<RequestBucketComposite> BucketTree { get; set; }

        public int StatusCode { get; set; }
    }
}
