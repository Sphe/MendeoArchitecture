using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Model.FullDomain
{
    public abstract class CategoryComponent
    {
        protected int id;
        protected string label;
        
        // Constructor
        public CategoryComponent(int id, string label)
        {
            this.id = id;
            this.label = label;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Label
        {
            get
            {
                return label;
            }
        }

        public string IconCssClass
        {
            get;
            set;
        }

        [JsonIgnore]
        public CategoryComponent Parent { get; set; }

        public IEnumerable<CategoryComponent> Parents()
        {
            CategoryComponent result = this;
            while (result.Parent != null)
            {
                result = result.Parent;
                yield return result;
            }
        }

        public CategoryComponent Root()
        {
            return Parents().LastOrDefault() ?? this;
        }

        public abstract IEnumerable<CategoryComponent> WalkTreeBreadthFirst();

        public abstract IEnumerable<CategoryComponent> WalkTreeDepthFirst();

        public abstract List<CategoryComponent> Children { get; set; }

        public abstract void Add(CategoryComponent c);

        public abstract void Remove(CategoryComponent c);
    }
}
