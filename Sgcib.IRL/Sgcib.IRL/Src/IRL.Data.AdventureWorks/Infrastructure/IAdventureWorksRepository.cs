﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Infrastructure;

namespace IRL.Data.AdventureWorks.Infrastructure
{
    public interface IAdventureWorksRepository<T> : IRepository<T>
        where T : class
    {
    }
}