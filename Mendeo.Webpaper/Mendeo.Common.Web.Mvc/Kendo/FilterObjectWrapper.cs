using System.Collections.Generic;

namespace Mendeo.Common.Web.Mvc.Kendo
{
    public class FilterObjectWrapper
    {
        public FilterObjectWrapper(string logic, IEnumerable<FilterObject> filterObjects)
        {
            Logic = logic;
            FilterObjects = filterObjects;
        }

        public string Logic { get; set; }
        public IEnumerable<FilterObject> FilterObjects { get; set; }
        public string LogicToken
        {
            get
            {
                switch (Logic)
                {
                    case "and":
                        return "&&";
                    case "or":
                        return "||";
                    default:
                        return null;
                }
            }
        }
    }
}
