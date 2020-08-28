using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class UsefulLinksContactViewModel
    {
        public ContactUsBanner ContactUsBanner { get; set; }

        public IList<LinksContacts> LinksContacts { get; set; }
    }
}
