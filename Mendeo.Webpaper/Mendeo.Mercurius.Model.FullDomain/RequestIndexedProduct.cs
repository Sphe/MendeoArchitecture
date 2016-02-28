using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class RequestIndexedProduct
    {
        public RequestIndexedProduct()
        {
            Filters = new List<RequestBucketComposite>();
            FilterCategoryId = 0;
        }

        public int From { get; set; }

        public int Size { get; set; }

        public int FromBucketTree { get; set; }

        public int SizeBucketTree { get; set; }

        public double SourceLat { get; set; }

        public double SourceLon { get; set; }

        public double Distance { get; set; }

        public string QuerySearch { get; set; }

        public int FilterCategoryId { get; set; }

        public int? UserId { get; set; }

        public IList<RequestBucketComposite> Filters { get; set; }

        public bool IsDemand { get; set; }

        public string Sort { get; set; }
    }
}
