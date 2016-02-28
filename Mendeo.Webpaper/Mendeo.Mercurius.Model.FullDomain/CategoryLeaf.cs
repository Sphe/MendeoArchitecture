using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class CategoryLeaf : CategoryComponent
    {
        // Constructor
        public CategoryLeaf(int id, string label)
            : base(id, label)
        {
        }

        public override void Add(CategoryComponent c)
        {
            throw new NotImplementedException();
        }

        public override void Remove(CategoryComponent c)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CategoryComponent> WalkTreeBreadthFirst()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CategoryComponent> WalkTreeDepthFirst()
        {
            throw new NotImplementedException();
        }

        public override List<CategoryComponent> Children
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
