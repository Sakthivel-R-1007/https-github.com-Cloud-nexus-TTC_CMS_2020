using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines.Web.Helpers;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;

namespace Tampines.Web.Controllers
{
    public class ResidentServicesController : Controller
    {
        #region Constructor And Private Members

        private IResidentServicesDao _residentServicesDao;

        private IBannerDao _bannerDao;
        public ResidentServicesController(IResidentServicesDao residentServicesDao, IBannerDao bannerDao)
        {
            _residentServicesDao = residentServicesDao;
            _bannerDao = bannerDao;


        }

        #endregion
        // GET: ResidentServices
        public ActionResult CommonAreaSpaces()
        {
            return View();
        }

        public ActionResult BulkyItemRemovalServices()
        {
            BulkyItemRemovalServices services = _residentServicesDao.GetBulkyItemRemovalServices();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.ResidentServicesBanner != null && viewBanner.ResidentServicesBanner.Count > 0)
                services.ResidentServicesBanner = viewBanner.ResidentServicesBanner.Where(s => s.Page == "Bulky Item Removal Services").FirstOrDefault();
            return View(services);
        }

        public ActionResult ServiceConservancyCharges()
        {
            return View();
        }
        public ActionResult TownCouncilServices()
        {
            return View();
        }

        public ActionResult MediateDisputes()
        {
            return View();
        }

        public ActionResult Downloads()
        {
            IList<Accordion> accordions = _residentServicesDao.GetPublishedAccordions();
            IList<Accordion> accordions1 = accordions.Where(m => m.IsPublished).ToList();
            return View(accordions1);
        }
    }
}