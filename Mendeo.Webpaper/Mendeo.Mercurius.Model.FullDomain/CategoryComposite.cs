using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public class CategoryComposite : CategoryComponent
    {
        private List<CategoryComponent> _children = new List<CategoryComponent>();

        // Constructor
        public CategoryComposite(int id, string label)
            : base(id, label)
        {
        }

        public override List<CategoryComponent> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        public override void Add(CategoryComponent component)
        {
            _children.Add(component);
        }

        public override void Remove(CategoryComponent component)
        {
            _children.Remove(component);
        }

        public override IEnumerable<CategoryComponent> WalkTreeBreadthFirst()
        {
            //http://en.wikipedia.org/wiki/Breadth-first_search
            HashSet<CategoryComponent> seenIt = new HashSet<CategoryComponent>();
            Queue<CategoryComponent> toVisit = new Queue<CategoryComponent>();
            toVisit.Enqueue(this);

            while (toVisit.Any())
            {
                CategoryComponent item = toVisit.Dequeue();
                if (!seenIt.Contains(item))
                {
                    seenIt.Add(item);
                    foreach (CategoryComponent child in _children)
                    {
                        toVisit.Enqueue(child);
                    }
                    yield return item;
                }
            }
        }

        public override IEnumerable<CategoryComponent> WalkTreeDepthFirst()
        {
            // http://en.wikipedia.org/wiki/Depth-first_search
            HashSet<CategoryComponent> seenIt = new HashSet<CategoryComponent>();
            Stack<CategoryComponent> toVisit = new Stack<CategoryComponent>();

            toVisit.Push(this);

            while (toVisit.Any())
            {
                CategoryComponent item = toVisit.Pop();
                if (!seenIt.Contains(item))
                {
                    seenIt.Add(item);
                    foreach (CategoryComponent child in _children.ToArray().Reverse())
                    {
                        toVisit.Push(child);
                    }
                    yield return item;
                }
            }
        }
    }
}
