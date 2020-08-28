using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class Events:Entity<Int64>
    {
        public string Title { get; set; }

        public string Date { get; set; }

        public DateTime EventDate { get; set; }
             

        public string FacebookLink { get; set; }

        public string InstagramLink { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public  string ImageGUID { get; set; }

        public string ImageName { get; set; }

        public string ImageExtension { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
        public List<EventSection> EventSection { get; set; }

        public Guid UserId { get; set; }

    }
}
