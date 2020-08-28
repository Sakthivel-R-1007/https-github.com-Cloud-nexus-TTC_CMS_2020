using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
   public  class OurMpsContents
    {
        public AboutUsBanner AboutUsBanner { get; set; }
        public OurMps OurMps { get; set; }

        public List<OurMps> OurMpList { get; set; }

    }
}
