using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;

namespace Tampines_CMS.Persistence.Interface
{
    public interface IBannerDao
    {

        ViewBannerModel GetViewBanner();

        Int64 SaveHomeBanner(HomeBanner HB);

        HomeBanner GetHomeBannerByGuid(Guid Id);

        Int64 DeleteHomeBanner(Guid GUID, Guid UserId, string SystemIp);
        IList<HomeBanner> GetHomeBanners();

        Int32 UpdateHomeBannerSorting(List<Sorting> sort, Guid UserId, string SystemIp);


        AboutUsBanner GetAboutUsBannerByGuid(Guid Id);
        Int64 UpdateAboutUsBanner(AboutUsBanner HB);


        OurTownBanner GetOurTownBannerByGuid(Guid Id);
        Int64 UpdateOurTownBanner(OurTownBanner HB);


        ResidentServicesBanner GetResidentServicesBannerByGuid(Guid Id);
        Int64 UpdateResidentServicesBanner(ResidentServicesBanner HB);

        TEMPOBanner GetTEMPOBannerByGuid(Guid Id);
        Int64 UpdateTEMPOBanner(TEMPOBanner HB);


        EventBanner GetEventBannerByGuid(Guid Id);
        Int64 UpdateEventBanner(EventBanner HB);


        ContactUsBanner GetContactUsBannerByGuid(Guid Id);
        Int64 UpdateContactUsBanner(ContactUsBanner HB);
    }



}
