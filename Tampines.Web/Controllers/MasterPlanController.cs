using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;

namespace Tampines.Web.Controllers
{
    public class MasterPlanController : Controller
    {

        #region Constructor And Private Members

        private IAboutUsDao _aboutUsDao;
        private IBannerDao _bannerDao;
        public MasterPlanController(IAboutUsDao aboutUsDao, IBannerDao bannerDao)
        {
            _aboutUsDao = aboutUsDao;
            _bannerDao = bannerDao;


        }

        #endregion
        // GET: MasterPlan
        public ActionResult Index()
        {
            return View();
        }


       
    }
}