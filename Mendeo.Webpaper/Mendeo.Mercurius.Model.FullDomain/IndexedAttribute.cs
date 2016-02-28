using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mendeo.Mercurius.Enum;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class IndexedAttribute
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Item { get; set; }

        public bool? ItemBool { get; set; }

        public DateTime? ItemDate { get; set; }

        public decimal? ItemNumber { get; set; }

        public ItemPrimitiveTypeEnum ItemPrimitiveType { get; set; }

        public string UnitLabel { get; set; }
    }
}
