using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class Accordion : Entity<Int64>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public Boolean IsPublished { get; set; }
        public IList<AccordionPdf> AccordionPdfS { get; set; }
    }
}
