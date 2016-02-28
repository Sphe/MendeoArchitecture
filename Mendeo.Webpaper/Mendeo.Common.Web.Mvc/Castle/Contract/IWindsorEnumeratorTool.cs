using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Web.Mvc.Castle.Contract
{
    public interface IWindsorEnumeratorTool
    {
        IList<string> GetAllControllerNames();

        IEnumerable<string> GetAllActionNames(string controller);
    }
}
