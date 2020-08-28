using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class EventSection
    {
        public string With { get; set; }
        public string Venue { get; set; }
        public string Time { get; set; }

        public Int64 EventsId { get; set; }
    }
}
