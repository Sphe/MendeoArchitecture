using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface IPeopleOnlineService
    {
        void SaveOrUpdatePeopleOnline(string ip, string userAgent, int? userId);

        void EnqueuePeopleOnline(string ip, string userAgent, int? userId);

        void ProcessQueuePeopleOnline(string subscriberId);

        void DisposeQueuePeopleOnline();
    }
}
