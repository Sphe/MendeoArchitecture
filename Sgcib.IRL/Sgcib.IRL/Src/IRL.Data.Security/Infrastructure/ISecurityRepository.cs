﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Infrastructure;

namespace IRL.Data.Security.Infrastructure
{
    public interface ISecurityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
