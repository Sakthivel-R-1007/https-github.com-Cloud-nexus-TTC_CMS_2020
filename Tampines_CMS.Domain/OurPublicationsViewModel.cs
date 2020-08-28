using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class OurPublicationsViewModel
    {
        public IList<AnnualReports> AnnualReports { get; set; }
        public IList<PressRelease> PressReleases { get; set; }
    }
}
