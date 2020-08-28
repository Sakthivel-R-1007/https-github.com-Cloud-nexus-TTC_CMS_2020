using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;

namespace Tampines.Web.Controllers
{
    public class AboutUsController : Controller
    {
        #region Constructor And Private Members

        private IAboutUsDao _aboutUsDao;
        private IBannerDao _bannerDao;
        public AboutUsController(IAboutUsDao aboutUsDao, IBannerDao bannerDao)
        {
            _aboutUsDao = aboutUsDao;
            _bannerDao = bannerDao;


        }

        #endregion
        // GET: AboutUs
        public ActionResult Introduction()
        {
            AboutUsIntroduction aboutUsIntroduction = _aboutUsDao.GetAboutUsIntroduction();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.AboutUsBanner != null && viewBanner.AboutUsBanner.Count > 0)
                aboutUsIntroduction.AboutUsBanner = viewBanner.AboutUsBanner.Where(s => s.Page == "Introduction").FirstOrDefault();
            return View(aboutUsIntroduction);
        }

        public ActionResult OurMPs()
        {
            OurMpsContents ourMpsContents = new OurMpsContents();
            List<OurMps> OurMps = _aboutUsDao.Get();

            if (OurMps != null && OurMps.Count > 0)
                ourMpsContents.OurMpList = OurMps;

            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.AboutUsBanner != null && viewBanner.AboutUsBanner.Count > 0)
                ourMpsContents.AboutUsBanner = viewBanner.AboutUsBanner.Where(s => s.Page == "Our MPs").FirstOrDefault();

            return View(ourMpsContents);
        }

        public ActionResult OrganisationChart()
        {
            return View();
        }

        public ActionResult OurPublications()
        {
            return View();
        }
    }
}