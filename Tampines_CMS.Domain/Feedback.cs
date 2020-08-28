using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string FeedbackType { get; set; }
        public int MPId { get; set; }
        public string MPName { get; set; }
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionFullName { get; set; }
        public string DivisionName { get; set; }
        public string Salutation { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string UnitNo { get; set; }
        public string Name { get; set; }
        public string NRIC { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string RoadName { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FileGuid { get; set; }
        public string PostalCode { get; set; }
        public string FeedbackText { get; set; }
        public string Subject { get; set; }

        public string SystemIp { get; set; }
    }
}
