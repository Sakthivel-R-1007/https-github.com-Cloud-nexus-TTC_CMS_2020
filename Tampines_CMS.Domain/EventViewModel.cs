using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class EventViewModel
    {
        public IList<EventBanner> EventBanner { get; set; }
        public IList<Events> Events { get; set; }
    }
}
