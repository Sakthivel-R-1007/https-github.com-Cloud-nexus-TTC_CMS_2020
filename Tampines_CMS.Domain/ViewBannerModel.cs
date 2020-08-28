using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class ViewBannerModel
    {
        public IList<HomeBanner> HomeBanner { get; set; }
        public IList<AboutUsBanner> AboutUsBanner { get; set; }

        public IList<OurTownBanner> OurTownBanner { get; set; }

        public IList<ResidentServicesBanner> ResidentServicesBanner { get; set; }

        public IList<TEMPOBanner> TEMPOBanner { get; set; }

        public IList<EventBanner> EventBanner { get; set; }

        public IList<ContactUsBanner> ContactUsBanner { get; set; }
    }
}

