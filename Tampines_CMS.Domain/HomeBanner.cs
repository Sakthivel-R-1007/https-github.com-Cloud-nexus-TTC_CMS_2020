using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class HomeBanner : Entity<Int64>
    {
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string ReadMore { get; set; }
        public string LinkTarget { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImageGUID { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public Int64 DisplayOrder { get; set; }
        public Guid UserId { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
    }
}
