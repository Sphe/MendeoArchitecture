using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Enum
{
    public enum ProductStatusTypeEnum
    {
        CREATED = 1,
        APPROVED = 2,
        MODIFIED = 3,
        REJECTED_REQUEST_MODIFY = 4,
        REJECTED_AND_DELETED = 5,
        EXPIRED = 6,
        DELETED = 7,
        PENDING_APPROVAL = 8,
        WAITING_MODIFY = 9,
        ERROR = 10,
        NOT_DEFINED = 11,
        NOTIFY_APPROVAL = 12,
        PERMANENT = 13
    }
}

