using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class FAQViewModel
    {
        public ContactUsBanner ContactUsBanner { get; set; }
        public IList<FAQ> FAQs { get; set; }
    }
}
