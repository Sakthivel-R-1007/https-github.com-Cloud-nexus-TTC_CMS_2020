using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class HomeViewModel
    {
        public IList<HomeBanner> HomeBanners { get; set; }

        public IList<Events> Events { get; set; }

        public List<OurMps> Mps { get; set; }

        public TownMapPDF mapPDF { get; set; }
    }
}
