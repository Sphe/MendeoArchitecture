using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class CategoryEntity
    {
        public Category Category { get; set; }

        public CategoryCultureMap CategoryCulture { get; set; }
    }
}
