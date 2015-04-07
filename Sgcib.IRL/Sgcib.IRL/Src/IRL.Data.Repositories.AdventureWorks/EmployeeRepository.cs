using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Data.Repositories.AdventureWorks.Contracts;
using IRL.Model.AdventureWorks;
using Common.Data.Infrastructure;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Linq.Dynamic;
using Common.Core;
using IRL.Data.AdventureWorks;
using IRL.Data.AdventureWorks.Infrastructure;

namespace IRL.Data.Repositories.AdventureWorks
{
    public class EmployeeRepository : AdventureWorksRepositoryBase<vEmployee>, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseFactory<AdventureWorks2012Entities> databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<MetadataModel> GetEmployeeMetadata()
        {
            var objContext = ((IObjectContextAdapter)DataContext).ObjectContext;
            var workspace = objContext.MetadataWorkspace;
            var tables = workspace.GetItems<EntityType>(DataSpace.CSpace).First();
            var vemployee = workspace.GetItem<EntityType>(tables.NamespaceName + "." + "vEmployee", DataSpace.CSpace);

            return (from field in vemployee.Members
                    select new MetadataModel
                    {
                        MemberName = field.Name,
                        MemberType = field.TypeUsage.ToString()
                    }).ToList();
        }

        public IList<vEmployee> GetEmployeesByMetadataAndFilter(IList<string> metadataToShow, string filterString, IList<FilterArgument> filterArguments)
        {
            var query = DbSet.Select(string.Format("new ({0})", string.Join(",", metadataToShow.ToArray())));

            if (!string.IsNullOrEmpty(filterString))
            {
                query = query.Where(filterString, ProcessFilterArguments(filterArguments)); 
            }

            var lstEmployee = new List<vEmployee>();

            foreach (dynamic item in query)
            {
                lstEmployee.Add(DynamicToStatic.ToStatic<vEmployee>(item));
            }

            return lstEmployee;
        }

        private object[] ProcessFilterArguments(IList<FilterArgument> filterArguments)
        {
            var lstFilterArgsConverted = new List<object>();
            foreach (var filterArgs in filterArguments)
            {
                lstFilterArgsConverted.Add(ProcessFilterArgument(filterArgs));
            }
            return lstFilterArgsConverted.ToArray();
        }

        private object ProcessFilterArgument(FilterArgument filterArgument)
        {
            switch (filterArgument.DataType)
            {
                case "Edm.Binary":
                    throw new NotImplementedException(string.Format("ProcessFilterArgument can't process the {0} data type", filterArgument.DataType));

                case "Edm.Boolean":
                    return Convert.ToBoolean(filterArgument.DataValue);

                case "Edm.Byte":
                    return Convert.ToByte(filterArgument.DataValue);

                case "Edm.DateTime":
                    return DateTime.Parse(filterArgument.DataValue);

                case "Edm.DateTimeOffset":
                    throw new NotImplementedException(string.Format("ProcessFilterArgument can't process the {0} data type", filterArgument.DataType));

                case "Edm.Decimal":
                    return Convert.ToDecimal(filterArgument.DataValue);
                
                case "Edm.Double":
                    return Convert.ToDouble(filterArgument.DataValue);

                case "Edm.Guid":
                    return Guid.Parse(filterArgument.DataValue);

                case "Edm.Int16":
                    return Convert.ToInt16(filterArgument.DataValue);

                case "Edm.Int32":
                    return Convert.ToInt32(filterArgument.DataValue);

                case "Edm.Int64":
                    return Convert.ToInt64(filterArgument.DataValue);

                case "Edm.SByte":
                    return Convert.ToSByte(filterArgument.DataValue);

                case "Edm.Single":
                    return Convert.ToSingle(filterArgument.DataValue);

                case "Edm.String":
                    return filterArgument.DataValue;

                case "Edm.Time":
                    throw new NotImplementedException(string.Format("ProcessFilterArgument can't process the {0} data type", filterArgument.DataType));

                default:
                    throw new NotImplementedException(string.Format("ProcessFilterArgument can't process the {0} data type", filterArgument.DataType));
            }
        }
    }
}
