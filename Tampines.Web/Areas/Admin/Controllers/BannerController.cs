using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines.Web.Filters;
using Tampines.Web.Helpers;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;

namespace Tampines.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
    public class BannerController : Controller
    {
        #region Constructor And Private Members

        private IBannerDao _bannerDao;


        public BannerController(IBannerDao bannerDao)
        {
            _bannerDao = bannerDao;

        }

        #endregion
        // GET: Admin/Banner
        public ActionResult ViewBanners()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            return View(viewBanner);
        }
        public ActionResult AddHomeBanner()
        {
            return View();
        }     

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddHomeBanner(HomeBanner HB)
        {

            if (HB != null && HB.Image != null && HB.Image.ContentLength > 0)
            {
              

                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/HomeBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.SaveHomeBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }

            }
            

            return View();
        }

        public ActionResult EditHomeBanner(string EncryptedId)
        {
            if(!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid=new Guid(EncryptedId);
                HomeBanner homeBanner = _bannerDao.GetHomeBannerByGuid(BannerGuid);
                return View(homeBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditHomeBanner(HomeBanner HB)
        {

            if (HB != null)
            {


                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/HomeBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.SaveHomeBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }

            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHomeBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Int64 Id = _bannerDao.DeleteHomeBanner(BannerGuid, UserId,SystemIp);
                TempData["data"] = "Changes Updated";
                return RedirectToRoute("ViewBanners");
            }
            return View();
        }

        public ActionResult SortHomeBanner()
        {
            IList<HomeBanner> banners = _bannerDao.GetHomeBanners();
            return View(banners);
        }

        [HttpPost]
        public ActionResult SortHomeBanner(List<Sorting> sort)
        {

            List<Sorting> sorting = new List<Sorting>();


            if (sort != null && sort.Count > 0)
            {
                foreach (Sorting so in sort)
                {
                    Sorting s = new Sorting();
                    if (so.GUID != null && so.GUID != Guid.Empty)
                    {
                        s.GUID = so.GUID;
                        s.ReOrder = so.ReOrder;
                        sorting.Add(s);
                    }
                }
                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Int32 result = _bannerDao.UpdateHomeBannerSorting(sorting, UserId, SystemIp);
            }
            TempData["data"] = "Changes Updated";
            return RedirectToRoute("ViewBanners");
        }


        public ActionResult EditAboutUsBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                AboutUsBanner homeBanner = _bannerDao.GetAboutUsBannerByGuid(BannerGuid);
                return View(homeBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditAboutUsBanner(AboutUsBanner HB)
        {

            if (HB != null)
            {
                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/AboutUsBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.UpdateAboutUsBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }
            }
            return View();
        }


        public ActionResult EditOurTownBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                OurTownBanner homeBanner = _bannerDao.GetOurTownBannerByGuid(BannerGuid);
                return View(homeBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditOurTownBanner(OurTownBanner HB)
        {

            if (HB != null)
            {
                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/OurTownBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.UpdateOurTownBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }
            }
            return View();
        }


        public ActionResult EditResidentServicesBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                ResidentServicesBanner homeBanner = _bannerDao.GetResidentServicesBannerByGuid(BannerGuid);
                return View(homeBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditResidentServicesBanner(ResidentServicesBanner HB)
        {

            if (HB != null)
            {
                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/ResidentServicesBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.UpdateResidentServicesBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }
            }
            return View();
        }

        public ActionResult EditTEMPOBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                TEMPOBanner homeBanner = _bannerDao.GetTEMPOBannerByGuid(BannerGuid);
                return View(homeBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditTEMPOBanner(TEMPOBanner HB)
        {

            if (HB != null)
            {
                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/TEMPOBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.UpdateTEMPOBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }
            }
            return View();
        }


        public ActionResult EditEventBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                EventBanner eventBanner = _bannerDao.GetEventBannerByGuid(BannerGuid);
                return View(eventBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditEventBanner(EventBanner HB)
        {

            if (HB != null)
            {
                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/EventBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.UpdateEventBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }
            }
            return View();
        }


        public ActionResult EditContactUsBanner(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid BannerGuid = new Guid(EncryptedId);
                ContactUsBanner contactUsBanner = _bannerDao.GetContactUsBannerByGuid(BannerGuid);
                return View(contactUsBanner);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditContactUsBanner(ContactUsBanner HB)
        {

            if (HB != null)
            {
                if (HB.Image != null && HB.Image.ContentLength > 0)
                {
                    HB.ImageExtension = Path.GetExtension(HB.Image.FileName).Trim('.');
                    HB.ImageName = Path.GetFileNameWithoutExtension(HB.Image.FileName);
                    HB.ImageGUID = Guid.NewGuid().ToString("N");
                    string imagelocation = Server.MapPath("~/Resources/Images/ContactUsBanner/" + HB.ImageGUID + "." + HB.ImageExtension);
                    HB.Image.SaveAs(imagelocation);
                }

                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                HB.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                HB.UserId = UserId;
                TempData["data"] = "Changes Updated";
                Int64 Id = _bannerDao.UpdateContactUsBanner(HB);
                if (Id > 0)
                {
                    return RedirectToRoute("ViewBanners");
                }
            }
            return View();
        }

    }
}