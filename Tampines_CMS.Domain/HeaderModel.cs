using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tampines_CMS.Domain
{
    public class HeaderModel
    {
        public TownMapPDF MapPDF { get; set; }

        public TownMapPDF OrganisationChartPDF { get; set; }

        public TownMapPDF MasterPlanPDF { get; set; }

        public TownMapPDF PDPAPDF { get; set; }

        public TownMapPDF WhistleBlowingPolicyPDF { get; set; }
    }
}
