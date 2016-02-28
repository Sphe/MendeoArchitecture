using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Enum;

namespace Mendeo.Mercurius.Dto
{
    public class ProductAttributeDto
    {
        public string Type { get; set; }

        public string Item { get; set; }

        public decimal? ItemNumber { get; set; }

        public bool? ItemBool { get; set; }

        public DateTime? ItemDate { get; set; }

        public ItemPrimitiveTypeEnum? FieldTypeId { get; set; }

        public string UnitLabel { get; set; }
    }
}
