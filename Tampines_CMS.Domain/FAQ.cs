using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class FAQ : Entity<Int64>
    {

        public string Question { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }

        public ContactUsBanner ContactUsBanner { get; set; }
    }
}
