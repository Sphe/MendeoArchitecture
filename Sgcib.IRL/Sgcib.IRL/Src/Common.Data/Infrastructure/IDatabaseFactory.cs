using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Common.Data.Infrastructure
{
    public interface IDatabaseFactory<T>
        where T : DbContext, new()
    {
        T Get();
    }
}
