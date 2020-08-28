using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class OurMps:Entity<Int64>
    {

        public string Name { get; set; }

        public string ImageGUID { get; set; }

        public string ImageName { get; set; }

        public string ImageExtension { get; set; }

        public string ImageAltTag { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string FacebookLink { get; set; }

        public string InstagramLink { get; set; }

        public string Division { get; set; }

        public string Designation { get; set; }

        public string MeetThePeople { get; set; }

        public string CommunityClub { get; set; }

      public string ModelClass { get; set; }

        public string Email { get; set; }

        public Guid UserId { get; set; }

    }
}
