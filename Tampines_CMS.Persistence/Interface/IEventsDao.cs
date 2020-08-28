using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;

namespace Tampines_CMS.Persistence.Interface
{
    public interface IEventsDao
    {
        int CheckEventTitleName(string EventTitle, Guid GUID);
        Int64 SaveEvents(Events E);
        IList<Events> Get(int PageIndex = 1, int PageSize = 10);
        Events GetEventByGuid(Guid GUID);        
        Int64 DeleteEvents(Guid GUID, Guid UserId, string SystemIp);
    }
}
