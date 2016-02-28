using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Enum
{
    public enum QueueStatusEnum
    {
        CREATED = 1,
        PROCESSING = 2,
        PROCESSED = 3,
        ERROR = 4,
        SUCCESS = 5
    }
}
