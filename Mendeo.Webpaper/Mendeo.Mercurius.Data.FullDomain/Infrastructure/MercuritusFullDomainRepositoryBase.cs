using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Data.FullDomain.Infrastructure
{
    public class MercuritusFullDomainRepositoryBase<T> : RepositoryBase<T, MercuriusEntities>, IMercuritusFullDomainRepository<T>
        where T : class
    {
        public MercuritusFullDomainRepositoryBase(IDatabaseFactory<MercuriusEntities> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
