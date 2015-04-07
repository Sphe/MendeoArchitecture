using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Infrastructure;

namespace IRL.Data.Security.Infrastructure
{
    public class SecurityUnitOfWorkBase : UnitOfWorkBase<IrlSecurityEntities>, ISecurityUnitOfWork
    {
        public SecurityUnitOfWorkBase(IDatabaseFactory<IrlSecurityEntities> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
