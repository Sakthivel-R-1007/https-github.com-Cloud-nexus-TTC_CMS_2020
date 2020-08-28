using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class AboutUsBanner : Entity<Int64>
    {
        public string Page { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImageGUID { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public Guid UserId { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
    }
}
