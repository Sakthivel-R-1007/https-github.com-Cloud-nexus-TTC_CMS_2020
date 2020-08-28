using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class OurTown : Entity<Int64>
    {
        public HttpPostedFileBase Image1 { get; set; }

        public string ImageGUID1 { get; set; }

        public string ImageName1 { get; set; }

        public string ImageExtension1 { get; set; }

        public string ImageAltTag1 { get; set; }

        public HttpPostedFileBase Image2 { get; set; }
        public string ImageGUID2 { get; set; }

        public string ImageName2 { get; set; }

        public string ImageExtension2 { get; set; }

        public string ImageAltTag2 { get; set; }
        public HttpPostedFileBase Image3 { get; set; }
        public string ImageGUID3 { get; set; }

        public string ImageName3 { get; set; }

        public string ImageExtension3 { get; set; }

        public string ImageAltTag3 { get; set; }

        public string Content { get; set; }

        public Guid UserId { get; set; }

        public OurTownBanner OurTownBanner { get; set; }
    }
}
