using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines_CMS.Persistence.Interface;
using Tampines_CMS.Domain;
using Tampines.Web.Helpers;

namespace Tampines.Web.Controllers
{
    public class ContactUsController : Controller
    {

        #region Constructor And Private Members

        private IContactsDao _contactUsDao;
        private IBannerDao _bannerDao;

        public ContactUsController(IContactsDao contactUsDao, IBannerDao bannerDao)
        {
            _contactUsDao = contactUsDao;
            _bannerDao = bannerDao;

        }

        #endregion
        // GET: ContactUs
        public ActionResult UsefulLinksContacts()
        {
            UsefulLinksContactViewModel contactViewModel = new UsefulLinksContactViewModel();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();

            IList<LinksContacts> LinksContacts = _contactUsDao.Get(0, 0);
            contactViewModel.LinksContacts = LinksContacts;
            if (viewBanner != null && viewBanner.ContactUsBanner != null && viewBanner.ContactUsBanner.Count > 0)
                contactViewModel.ContactUsBanner = viewBanner.ContactUsBanner.Where(s => s.Page == "Useful Links & Contacts").FirstOrDefault();

            return View(contactViewModel);
        }


        public ActionResult GettingHere()
        {
            return View();
        }

        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult FAQs()
        {
            FAQViewModel viewModel = new FAQViewModel();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();

            IList<FAQ> fAQs = _contactUsDao.GetFAQ(0, 0);
            if (fAQs != null && fAQs.Count > 0)
                viewModel.FAQs = fAQs;
            if (viewBanner != null && viewBanner.ContactUsBanner != null && viewBanner.ContactUsBanner.Count > 0)
                viewModel.ContactUsBanner = viewBanner.ContactUsBanner.Where(s => s.Page == "FAQs").FirstOrDefault();
            return View(viewModel);
        }


        public JsonResult RefreshCaptcha()
        {
            return Json(new { captchaImage = Captcha.GetBase64(HttpContext), code = Convert.ToString(Session["Captcha_FB"]) }, JsonRequestBehavior.AllowGet);
        }
    }
}