using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core;
using System.Data.Entity;
using Common.Data.Infrastructure;

namespace IRL.Data.AdventureWorks.Infrastructure
{
    public class AdventureWorksDatabaseFactory : Disposable, IDatabaseFactory<AdventureWorks2012Entities>
    {
        private AdventureWorks2012Entities dataContext;
        private readonly object syncObject = new object();

        public AdventureWorks2012Entities Get()
        {
            if (dataContext == null)
            {
                lock (syncObject)
                {
                    if (dataContext == null)
                    {
                        dataContext = new AdventureWorks2012Entities();
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
