using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
   public  class LinksContacts: Entity<Int64>
    {

        public  string Organisation { get; set; }

        public string Issue { get; set; }

        public string Contact { get; set; }
        public Guid UserId { get; set; }
        public Int64 RowNum { get; set; }

        public Int64 TotalCount { get; set; }
        public ContactUsBanner ContactUsBanner { get; set; }
    }
}
