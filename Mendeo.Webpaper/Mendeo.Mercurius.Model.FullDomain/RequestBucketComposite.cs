using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class RequestBucketComposite
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public long DocumentCount { get; set; }

        public bool Selected { get; set; }

        public BucketTransformationEnum BucketTransformationType { get; set; }

        public ItemPrimitiveTypeEnum PrimitiveType { get; set; }

        public decimal? NumberMinRangeValue { get; set; }

        public decimal? NumberMaxRangeValue { get; set; }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }

        public double? MinFloorValue { get; set; }

        public double? MaxCeilValue { get; set; }

        public string StringValue { get; set; }

        public bool? BoolValue { get; set; }

        public DateTime? DateMinRangeValue { get; set; }

        public DateTime? DateMaxRangeValue { get; set; }

        public IList<RequestBucketComposite> Childrens { get; set; }
    }
}
