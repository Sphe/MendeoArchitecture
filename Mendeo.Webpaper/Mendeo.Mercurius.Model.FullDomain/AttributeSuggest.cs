using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class AttributeSuggest
    {
        public AttributeType Type { get; set; }

        public IList<AttributeItem> Items { get; set; }

        public int LinkedProductScore { get; set; }

        public int PresenceAttributeScore { get; set; }

        public int LinkedCategoryScore { get; set; }
    }
}
