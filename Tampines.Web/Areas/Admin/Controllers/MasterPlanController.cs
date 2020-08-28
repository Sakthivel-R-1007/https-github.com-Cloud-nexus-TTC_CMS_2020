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
        // GET: Admin/MasterPlan
        public ActionResult Index()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            TownMapPDF town = _aboutUsDao.GetMasterPlanPDF();
            return View(town);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TownMapPDF T)
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

            Int64 Id = _aboutUsDao.SaveMasterPlanPDF(T);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}