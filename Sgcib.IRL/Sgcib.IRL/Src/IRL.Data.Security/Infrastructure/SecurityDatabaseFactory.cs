using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core;
using System.Data.Entity;
using Common.Data.Infrastructure;
using IRL.Data.Security;
using System.Diagnostics;
using System.Threading;

namespace IRL.Data.Security.Infrastructure
{
    public class SecurityDatabaseFactory : Disposable, IDatabaseFactory<IrlSecurityEntities>
    {
        private IrlSecurityEntities dataContext;
        private readonly object syncObject = new object();

        public IrlSecurityEntities Get()
        {
            if (dataContext == null)
            {
                lock (syncObject)
                {
                    if (dataContext == null)
                    {
                        dataContext = new IrlSecurityEntities();
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
    }
}
