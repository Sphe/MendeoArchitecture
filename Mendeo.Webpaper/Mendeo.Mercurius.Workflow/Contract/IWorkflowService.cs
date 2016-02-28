using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Workflow.Contract
{
    public interface IWorkflowService
    {
        void ExecuteProductMainWorkflow(int productId);
    }
}
