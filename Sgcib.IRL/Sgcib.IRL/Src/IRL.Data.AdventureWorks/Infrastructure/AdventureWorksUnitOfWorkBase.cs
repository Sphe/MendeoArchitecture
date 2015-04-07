using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Infrastructure;

namespace IRL.Data.AdventureWorks.Infrastructure
{
    public class AdventureWorksUnitOfWorkBase : UnitOfWorkBase<AdventureWorks2012Entities>, IAdventureWorksUnitOfWork
    {
        public AdventureWorksUnitOfWorkBase(IDatabaseFactory<AdventureWorks2012Entities> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
