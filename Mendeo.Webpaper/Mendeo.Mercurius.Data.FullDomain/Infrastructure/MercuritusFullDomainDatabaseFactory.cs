using Common.Core;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Data.FullDomain.Infrastructure
{
    public class MercuritusFullDomainDatabaseFactory : Disposable, IDatabaseFactory<MercuriusEntities>
    {
        private MercuriusEntities dataContext = null;
        private readonly object syncObject = new object();

        public MercuriusEntities Get()
        {
            if (dataContext == null)
            {
                lock (syncObject)
                {
                    if (dataContext == null)
                    {
                        dataContext = new MercuriusEntities();
                    }
                }
            }
            return dataContext;
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }

        public void Refresh()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
                dataContext = new MercuriusEntities();
            }
        }
    }
}
