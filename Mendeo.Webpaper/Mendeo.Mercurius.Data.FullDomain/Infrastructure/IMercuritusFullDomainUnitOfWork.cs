using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Data.FullDomain.Infrastructure
{
    public interface IMercuritusFullDomainUnitOfWork : IUnitOfWork
    {
        IList<Tuple<string, string>> CommitHandled();
    }
}
