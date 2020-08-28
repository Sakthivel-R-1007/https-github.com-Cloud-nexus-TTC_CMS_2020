using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;

namespace Tampines_CMS.Persistence.Interface
{
    public interface IAboutUsDao
    {
        AboutUsIntroduction GetAboutUsIntroduction();
        Int64 Save(AboutUsIntroduction aboutUsIntroduction);

        TownMapPDF GetOrganisationChartPDF();

        Int64 SaveOrganisationChartPDF(TownMapPDF townMap);


        TownMapPDF GetMasterPlanPDF();

        Int64 SaveMasterPlanPDF(TownMapPDF townMap);



        List<OurMps> Get();

        Int64 SaveOurMps(OurMps LC);
        OurMps GetOurMpsByGuid(Guid GUID);
        Int64 DeleteOurMps(Guid GUID, Guid UserId, string SystemIp);
        IList<OurMps> GetOurMpsSorting();

        Int32 UpdateOurMpsSorting(List<Sorting> sort, Guid UserId, string SystemIp);


        OurPublicationsViewModel GetOurPublications();

        long Save(AnnualReports AU, Guid UserGUID);
        int Delete(AnnualReports AU, Guid UserGUID);
        AnnualReports GetbyGuid(Guid GUID);
       


       
       

        Int64 SavePressRelease(PressRelease PR, Guid UserGUID);
        int DeletePress(PressRelease PR, Guid UserGUID);
        PressRelease GetbyGuidPress(Guid GUID);



    }
}
