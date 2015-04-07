using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.AdventureWorks;
using IRL.Data.AdventureWorks.Infrastructure;

namespace IRL.Data.Repositories.AdventureWorks.Contracts
{
    public interface IEmployeeRepository : IAdventureWorksRepository<vEmployee>
    {
        IList<MetadataModel> GetEmployeeMetadata();
        IList<vEmployee> GetEmployeesByMetadataAndFilter(IList<string> metadataToShow, string filterString, IList<FilterArgument> filterArguments);
    }
}
