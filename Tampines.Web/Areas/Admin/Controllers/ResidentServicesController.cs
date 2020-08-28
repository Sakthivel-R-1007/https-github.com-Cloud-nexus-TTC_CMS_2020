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
        // GET: Admin/ResidentServices
        public ActionResult UseOfCommoAreaOpenSpaces()
        {
            return View();
        }

        public ActionResult AddFunction()
        {
            return View();
        }

        public ActionResult EditFunction()
        {
            return View();
        }



        public ActionResult BulkyItemRemovalServices()
        {
            BulkyItemRemovalServices aboutUsIntroduction = _residentServicesDao.GetBulkyItemRemovalServices();
            return View(aboutUsIntroduction);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult BulkyItemRemovalServices(BulkyItemRemovalServices services)
        {
            if (services != null)
            {

                services.UserId = ((UserAccount)Session["UserAccount"]).GUID;
                services.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Int64 Id = _residentServicesDao.Save(services);
                return RedirectToAction("BulkyItemRemovalServices");
            }

            return View();
        }


        public ActionResult BulkyItemRemovalServicesPreview()
        {
            return RedirectToAction("BulkyItemRemovalServices");
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult BulkyItemRemovalServicesPreview(BulkyItemRemovalServices services)
        {
            if (services != null)
            {

                services.UserId = ((UserAccount)Session["UserAccount"]).GUID;
                services.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
                if (viewBanner != null && viewBanner.ResidentServicesBanner != null && viewBanner.ResidentServicesBanner.Count > 0)
                    services.ResidentServicesBanner = viewBanner.ResidentServicesBanner.Where(s => s.Page == "Bulky Item Removal Services").FirstOrDefault();
                return View(services);
            }

            return View();
        }
        public ActionResult ServiceConservancyCharges()
        {
            return View();
        }
        public ActionResult Downloads()
        {
            return View(_residentServicesDao.GetAccordions());
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccordion(string EncDetail)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                Accordion A = new Accordion();
                A.GUID = new Guid(EncDetail);
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                _residentServicesDao.DeleteAccordion(A, new Guid(GUID));

            }
            return RedirectToAction("Downloads");
        }

        public ActionResult AddAccordion()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccordion(Accordion A)
        {
            if (A != null)
            {
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                A.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Accordion accordion = _residentServicesDao.SaveAccordion(A, new Guid(GUID));
                return RedirectToAction("EditAccordion", new { EncDetail = accordion.GUID });
            }
            return View();
        }

        public ActionResult EditAccordion(string EncDetail)
        {
            Accordion A = _residentServicesDao.GetAccordionByGuid(!string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty);
            return View(A);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccordion(Accordion A)
        {
            if (A != null)
            {
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                A.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Accordion accordion = _residentServicesDao.SaveAccordion(A, new Guid(GUID));
                //return RedirectToAction("EditAccordion", new { EncDetail = accordion.GUID });
                return RedirectToAction("Downloads");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPDF(AccordionPdf A, HttpPostedFileBase PdfFile)
        {
            string location = string.Empty;
            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            if (A != null)
            {
                if (PdfFile != null && PdfFile.ContentLength > 0)
                {
                    A.Extension = Path.GetExtension(PdfFile.FileName).Trim('.');
                    A.FileName = Path.GetFileNameWithoutExtension(PdfFile.FileName);
                    A.FileGUID = Guid.NewGuid().ToString("N");
                    location = Server.MapPath("~/Resources/Documents/ResidentServices/" + A.FileGUID + "." + A.Extension);
                    PdfFile.SaveAs(location);
                }
                A.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                int id = _residentServicesDao.SaveAccordionPdf(A, new Guid(GUID));
                return RedirectToAction("EditAccordion", new { EncDetail = A.AccordionGuid });
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPDF(AccordionPdf A, HttpPostedFileBase PdfFile)
        {
            string location = string.Empty;
            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            if (A != null)
            {
                if (PdfFile != null && PdfFile.ContentLength > 0)
                {
                    A.Extension = Path.GetExtension(PdfFile.FileName).Trim('.');
                    A.FileName = Path.GetFileNameWithoutExtension(PdfFile.FileName);
                    A.FileGUID = Guid.NewGuid().ToString("N");
                    location = Server.MapPath("~/Resources/Documents/ResidentServices/" + A.FileGUID + "." + A.Extension);
                    PdfFile.SaveAs(location);
                }
                A.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                int id = _residentServicesDao.SaveAccordionPdf(A, new Guid(GUID));
                return RedirectToAction("EditAccordion", new { EncDetail = A.AccordionGuid });
                
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccordionPdf(string EncDetail, string AccordionGuid)
        {
            if (!string.IsNullOrEmpty(EncDetail))
            {
                AccordionPdf A = new AccordionPdf();
                A.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                A.GUID = new Guid(EncDetail);
                string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
                _residentServicesDao.DeleteAccordionPdf(A, new Guid(GUID));
            }
            return RedirectToAction("EditAccordion", new { EncDetail = AccordionGuid });
        }

        [HttpGet]
        public PartialViewResult EditAccordionPdfPartialView(string EncDetail = null, string EncGUID = null)
        {
            AccordionPdf AP = _residentServicesDao.GetAccordionPdfByGuid(!string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty);
            AP.AccordionGuid = new Guid(EncGUID);
            return PartialView(AP);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult PublishAccordionToLive()
        {
            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            _residentServicesDao.PublishAccordionToLive(new Guid(GUID), SystemIp);
            return RedirectToAction("Downloads");
        }

        [HttpGet]
        public ActionResult PreviewAccordion(string Encdetail=null)
        {
            return RedirectToAction("Downloads");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult PreviewAccordion()
        {
            IList<Accordion> accordions = _residentServicesDao.GetPublishedAccordions();
            return View(accordions);
        }
  
        public ActionResult CheckAccordionTitle(string Title, string EncDetail)
        {
            int result = CheckAccordionTitleIsValid(Title, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }
        public int CheckAccordionTitleIsValid(string Title, string EncDetail)
        {
            string TitleName = (!string.IsNullOrEmpty(Title) ? Title : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) ? new Guid(EncDetail) : Guid.Empty;
            int result = _residentServicesDao.CheckAccordionTitleIsValid(TitleName, GUID);
            return result;
        }



    }
}