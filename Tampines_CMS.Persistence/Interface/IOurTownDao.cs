using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;

namespace Tampines_CMS.Persistence.Interface
{
    public interface IOurTownDao
    {
        OurTown GetOurTown();
        Int64 Save(OurTown ourTown);

        TownMapPDF GetTownMapPDF();

        Int64 SavePDF(TownMapPDF townMap);
    }
}
