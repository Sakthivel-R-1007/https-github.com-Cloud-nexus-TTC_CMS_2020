using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
   public  class AccordionPdf : Entity<Int64>
    {
        public Int64 AccordionId { get; set; }
        public string PdfTitle { get; set; }
        public string FileGUID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public Guid AccordionGuid { get; set; }
    }
}
