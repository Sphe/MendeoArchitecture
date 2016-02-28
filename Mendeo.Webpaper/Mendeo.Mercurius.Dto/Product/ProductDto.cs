using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string AdressOneLine { get; set; }

        public string Country { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public int CategoryId { get; set; }

        public IList<ProductAttributeDto> ProductAttributes { get; set; }

        public IList<ProductImageDto> ProductImages { get; set; }

        public IList<ValidationError> Errors { get; set; }

        public bool IsPro { get; set; }

        public bool IsDemand { get; set; }

        public IList<Guid> ImageIdentifiersToDelete { get; set; } 
    }
}
