using Mendeo.Mercurius.Enum;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    [ElasticType(Name = "indexedProduct")]
    public class IndexedProduct
    {
        public int Id { get; set; }

        public IList<int> CategoryIds { get; set; }

        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public IList<IndexedImage> Images { get; set; }

        [ElasticProperty(Type = FieldType.GeoPoint)]
        public IndexedCoordinate Coordinate { get; set; }

        [ElasticProperty(Type = FieldType.Nested, Name = "attributes")]
        public IList<IndexedAttribute> Attributes { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UserId { get; set; }

        public ProductStatusTypeEnum LastStatus { get; set; }

        public string ResearchField { get; set; }

        public int? ProductAnnounceTypeID { get; set; }
    }
}
