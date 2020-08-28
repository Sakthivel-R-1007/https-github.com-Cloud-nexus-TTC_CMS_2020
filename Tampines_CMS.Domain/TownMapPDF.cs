using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class TownMapPDF : Entity<Int64>
    {
        public HttpPostedFileBase PDFFile { get; set; }

        public string PDFFileGUID { get; set; }

        public string PDFFileName { get; set; }

        public string PDFFileExtension { get; set; }


        public Guid UserId { get; set; }
    }
}
