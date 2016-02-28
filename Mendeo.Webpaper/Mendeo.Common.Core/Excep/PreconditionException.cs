using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Common.Core
{
    public class PreconditionException : Exception
    {
        public PreconditionException(string message) : base(message)
        {
        }

        public PreconditionException(string message, Exception inner)
            : base(message, inner)
        {
        } 
    }
}
