using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common.Data.Infrastructure;

using IRL.Model.AdventureWorks;
using IRL.Services.Contracts;
using IRL.Data.Repositories.AdventureWorks.Contracts;
using IRL.Model.Security;

namespace IRL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IList<MetadataModel> EmployeeMetadata()
        {
            return employeeRepository.GetEmployeeMetadata()
                                     .OrderBy(x => x.MemberName)
                                     .ToList();
        }

        public IList<vEmployee> GetEmployeesByMetadataAndFilter(IList<string> metadataToShow, string filterString, IList<FilterArgument> filterArguments)
        {
            return employeeRepository.GetEmployeesByMetadataAndFilter(metadataToShow, filterString, filterArguments);
        }
    }
}
