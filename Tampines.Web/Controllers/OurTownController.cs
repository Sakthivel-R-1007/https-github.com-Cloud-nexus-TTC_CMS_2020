using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;

namespace Tampines.Web.Controllers
{
    public class OurTownController : Controller
    {
        #region Constructor And Private Members

        private IOurTownDao _ourTownDao;

        private IBannerDao _bannerDao;
        public OurTownController(IOurTownDao ourTownDao, IBannerDao bannerDao)
        {
            _ourTownDao = ourTownDao;
            _bannerDao = bannerDao;


        }

        #endregion
        // GET: OurTown
        public ActionResult Introduction()
        {
            OurTown ourTown = _ourTownDao.GetOurTown();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.OurTownBanner != null && viewBanner.OurTownBanner.Count > 0)
                ourTown.OurTownBanner = viewBanner.OurTownBanner.Where(s => s.Page == "Introduction").FirstOrDefault();
            return View(ourTown);
        }

        public ActionResult TownMap()
        {
            return View();
        }

        public ActionResult CyclingTown()
        {
            OurTownBanner banner = new OurTownBanner();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.OurTownBanner != null && viewBanner.OurTownBanner.Count > 0)
            {
                 banner = viewBanner.OurTownBanner.Where(s => s.Page == "Cycling Town").FirstOrDefault();
            }
            return View(banner);
        }

        public ActionResult EcoTown()
        {
            OurTownBanner banner = new OurTownBanner();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.OurTownBanner != null && viewBanner.OurTownBanner.Count > 0)
            {
                banner = viewBanner.OurTownBanner.Where(s => s.Page == "ECO Town").FirstOrDefault();
            }
            return View(banner);
        }
    }
}