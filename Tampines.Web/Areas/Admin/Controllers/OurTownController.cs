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
        // GET: Admin/OurTown
        public ActionResult Introduction()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            OurTown ourTown = _ourTownDao.GetOurTown();
            return View(ourTown);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Introduction(OurTown OT)
        {
            if (OT.Image1 != null && OT.Image1.ContentLength > 0)
            {
                OT.ImageExtension1 = Path.GetExtension(OT.Image1.FileName).Trim('.');
                OT.ImageName1 = Path.GetFileNameWithoutExtension(OT.Image1.FileName);
                OT.ImageGUID1 = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/OurTown/" + OT.ImageGUID1 + "." + OT.ImageExtension1);
                OT.Image1.SaveAs(ThumbnailImagelocation);

            }
            if (OT.Image2 != null && OT.Image2.ContentLength > 0)
            {
                OT.ImageExtension2 = Path.GetExtension(OT.Image2.FileName).Trim('.');
                OT.ImageName2 = Path.GetFileNameWithoutExtension(OT.Image2.FileName);
                OT.ImageGUID2 = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/OurTown/" + OT.ImageGUID2 + "." + OT.ImageExtension2);
                OT.Image2.SaveAs(ThumbnailImagelocation);

            }
            if (OT.Image3 != null && OT.Image3.ContentLength > 0)
            {
                OT.ImageExtension3 = Path.GetExtension(OT.Image3.FileName).Trim('.');
                OT.ImageName3 = Path.GetFileNameWithoutExtension(OT.Image3.FileName);
                OT.ImageGUID3 = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/OurTown/" + OT.ImageGUID3 + "." + OT.ImageExtension3);
                OT.Image3.SaveAs(ThumbnailImagelocation);

            }
            OT.UserId = ((UserAccount)Session["UserAccount"]).GUID;
            OT.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            Int64 Id = _ourTownDao.Save(OT);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("Introduction");
            }
            return View();
        }


        public ActionResult Preview()
        {
            return RedirectToAction("Introduction");
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Preview(OurTown OT)
        {
            if (OT.Image1 != null && OT.Image1.ContentLength > 0)
            {
                OT.ImageExtension1 = Path.GetExtension(OT.Image1.FileName).Trim('.');
                OT.ImageName1 = Path.GetFileNameWithoutExtension(OT.Image1.FileName);
                OT.ImageGUID1 = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/OurTown/" + OT.ImageGUID1 + "." + OT.ImageExtension1);
                OT.Image1.SaveAs(ThumbnailImagelocation);

            }
            if (OT.Image2 != null && OT.Image2.ContentLength > 0)
            {
                OT.ImageExtension2 = Path.GetExtension(OT.Image2.FileName).Trim('.');
                OT.ImageName2 = Path.GetFileNameWithoutExtension(OT.Image2.FileName);
                OT.ImageGUID2 = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/OurTown/" + OT.ImageGUID2 + "." + OT.ImageExtension2);
                OT.Image2.SaveAs(ThumbnailImagelocation);

            }
            if (OT.Image3 != null && OT.Image3.ContentLength > 0)
            {
                OT.ImageExtension3 = Path.GetExtension(OT.Image3.FileName).Trim('.');
                OT.ImageName3 = Path.GetFileNameWithoutExtension(OT.Image3.FileName);
                OT.ImageGUID3 = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/OurTown/" + OT.ImageGUID3 + "." + OT.ImageExtension3);
                OT.Image3.SaveAs(ThumbnailImagelocation);

            }
            OT.UserId = ((UserAccount)Session["UserAccount"]).GUID;
            OT.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.OurTownBanner != null && viewBanner.OurTownBanner.Count > 0)
                OT.OurTownBanner = viewBanner.OurTownBanner.Where(s => s.Page == "Introduction").FirstOrDefault();

            return View(OT);
        }

        public ActionResult TownMap()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            TownMapPDF town = _ourTownDao.GetTownMapPDF();
            return View(town);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TownMap(TownMapPDF T)
        {
            if (T.PDFFile != null && T.PDFFile.ContentLength > 0)
            {
                T.PDFFileExtension = Path.GetExtension(T.PDFFile.FileName).Trim('.');
                T.PDFFileName = Path.GetFileNameWithoutExtension(T.PDFFile.FileName);
                T.PDFFileGUID = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Documents/OurTown/" + T.PDFFileGUID + "." + T.PDFFileExtension);
                T.PDFFile.SaveAs(ThumbnailImagelocation);

            }
            T.UserId = ((UserAccount)Session["UserAccount"]).GUID;
            T.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

            Int64 Id = _ourTownDao.SavePDF(T);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("TownMap");
            }
            return View();
        }


    }
}