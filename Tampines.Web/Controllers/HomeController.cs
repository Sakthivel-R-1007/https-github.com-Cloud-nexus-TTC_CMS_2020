using Sita_Aircraft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines.Web.Helpers;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;
using Tampines_CMS.Service.Interfaces;

namespace Tampines.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor And Private Members

        private IEventsDao _EventsDao;
        private IBannerDao _bannerDao;
        private IOurTownDao _ourTownDao;
        private IAboutUsDao _aboutUsDao;
        private IContactsDao _contactsDao;

        public HomeController(IEventsDao EventsDao, IBannerDao bannerDao, IOurTownDao ourTownDao, IAboutUsDao aboutUsDao, IContactsDao contactsDao)
        {
            _EventsDao = EventsDao;
            _bannerDao = bannerDao;
            _ourTownDao = ourTownDao;
            _aboutUsDao = aboutUsDao;
            _contactsDao = contactsDao;

        }

        #endregion
        public ActionResult Index()
        {
            HomeViewModel homeView = new HomeViewModel();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            IList<Events> events = _EventsDao.Get(1, 10);
            if (viewBanner != null && viewBanner.HomeBanner != null && viewBanner.HomeBanner.Count > 0)
                homeView.HomeBanners = viewBanner.HomeBanner;
            if (events != null && events.Count > 0)
                homeView.Events = events.Take(3).ToList();
            TownMapPDF town = _ourTownDao.GetTownMapPDF();
            if (town != null && !string.IsNullOrEmpty(town.PDFFileGUID))
                homeView.mapPDF = town;
            List<OurMps> OurMps = _aboutUsDao.Get();
            if (OurMps != null && OurMps.Count > 0)
                homeView.Mps = OurMps;


            return View(homeView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            HeaderModel model = new HeaderModel();

            TownMapPDF town = _ourTownDao.GetTownMapPDF();
            if (town != null && !string.IsNullOrEmpty(town.PDFFileGUID))
            {
                model.MapPDF = town;
            }
            TownMapPDF OrganisationChartPDF = _aboutUsDao.GetOrganisationChartPDF();

            if (OrganisationChartPDF != null && !string.IsNullOrEmpty(OrganisationChartPDF.PDFFileGUID))
            {
                model.OrganisationChartPDF = OrganisationChartPDF;
            }

            TownMapPDF MasterPlanPDF = _aboutUsDao.GetMasterPlanPDF();

            if (MasterPlanPDF != null && !string.IsNullOrEmpty(MasterPlanPDF.PDFFileGUID))
            {
                model.MasterPlanPDF = MasterPlanPDF;
            }

            TownMapPDF GetPDPAPDF = _contactsDao.GetPDPAPDF();

            if (GetPDPAPDF != null && !string.IsNullOrEmpty(GetPDPAPDF.PDFFileGUID))
            {
                model.PDPAPDF = GetPDPAPDF;
            }

            TownMapPDF WhistleBlowingPolicyPDF = _contactsDao.GetWhistleBlowingPolicyPDF();

            if (WhistleBlowingPolicyPDF != null && !string.IsNullOrEmpty(WhistleBlowingPolicyPDF.PDFFileGUID))
            {
                model.WhistleBlowingPolicyPDF = WhistleBlowingPolicyPDF;
            }
            // initialize a model
            return PartialView("_Header", model);
        }

    }
}