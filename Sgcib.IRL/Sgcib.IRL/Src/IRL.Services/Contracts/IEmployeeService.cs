using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.AdventureWorks;

namespace IRL.Services.Contracts
{
    public interface IEmployeeService
    {
        IList<MetadataModel> EmployeeMetadata();
        IList<vEmployee> GetEmployeesByMetadataAndFilter(IList<string> metadataToShow, string filterString, IList<FilterArgument> filterArguments);
    }
}
