using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Infrastructure;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;

namespace IRL.Data.AdventureWorks.Infrastructure
{
    public class AdventureWorksRepositoryBase<T> : RepositoryBase<T, AdventureWorks2012Entities>, IAdventureWorksRepository<T>
        where T : class
    {
        public AdventureWorksRepositoryBase(IDatabaseFactory<AdventureWorks2012Entities> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
