using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Infrastructure;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using IRL.Data.Security;

namespace IRL.Data.Security.Infrastructure
{
    public class SecurityRepositoryBase<T> : RepositoryBase<T, IrlSecurityEntities>, ISecurityRepository<T>
        where T : class
    {
        public SecurityRepositoryBase(IDatabaseFactory<IrlSecurityEntities> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
