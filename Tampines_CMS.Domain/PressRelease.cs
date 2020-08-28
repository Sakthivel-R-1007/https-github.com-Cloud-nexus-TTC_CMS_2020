using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tampines_CMS.Domain
{
    public class PressRelease : Entity<Int64>
    {
        public string Date { get; set; }

        public DateTime PressDate { get; set; }
        public string Title { get; set; }
        public string ImageAltTag { get; set; }
        public HttpPostedFileBase FileName { get; set; }
        public string PDFGUID { get; set; }
        public string PDFName { get; set; }
        public string PDFExtension { get; set; }
      
        public string PDFFileSize { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 TotalCount { get; set; }
    }
}
