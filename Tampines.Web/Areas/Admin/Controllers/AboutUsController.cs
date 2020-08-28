using System;
using System.Collections.Generic;
using System.Globalization;
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
        // GET: Admin/AboutUs
        public ActionResult Introduction()
        {
            AboutUsIntroduction aboutUsIntroduction = _aboutUsDao.GetAboutUsIntroduction();
            return View(aboutUsIntroduction);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Introduction(AboutUsIntroduction aboutUs)
        {
            if (aboutUs != null)
            {

                aboutUs.UserId = ((UserAccount)Session["UserAccount"]).GUID;
                aboutUs.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Int64 Id = _aboutUsDao.Save(aboutUs);
                return RedirectToAction("Introduction");
            }

            return View();
        }

        public ActionResult IntroductionPreview()
        {
            return RedirectToAction("Introduction");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult IntroductionPreview(AboutUsIntroduction aboutUs)
        {
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.AboutUsBanner != null && viewBanner.AboutUsBanner.Count > 0)
                aboutUs.AboutUsBanner = viewBanner.AboutUsBanner.Where(s => s.Page == "Introduction").FirstOrDefault();
            return View(aboutUs);
        }




        #region 
        public ActionResult OurMPs()
        {

            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            List<OurMps> OurMps = _aboutUsDao.Get();
            return View(OurMps);
        }
        public ActionResult EditMPs(string EncryptedId)
        {

            if (!string.IsNullOrEmpty(EncryptedId))
            {
                OurMps OurMps = _aboutUsDao.GetOurMpsByGuid(new Guid(EncryptedId));
                return View(OurMps);
            }
            return View();
        }

        public ActionResult Preview()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Preview(OurMps OM)
        {
            if (OM.Image != null && OM.Image.ContentLength > 0)
            {
                OM.ImageExtension = Path.GetExtension(OM.Image.FileName).Trim('.');
                OM.ImageName = Path.GetFileNameWithoutExtension(OM.Image.FileName);
                OM.ImageGUID = Guid.NewGuid().ToString("N");
                string Imagelocation = Server.MapPath("~/Resources/Images/OurMps/" + OM.ImageGUID + "." + OM.ImageExtension);
                OM.Image.SaveAs(Imagelocation);

            }

            OurMpsContents ourMps = new OurMpsContents();
            ourMps.OurMps = OM;
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.AboutUsBanner != null && viewBanner.AboutUsBanner.Count > 0)
                ourMps.AboutUsBanner = viewBanner.AboutUsBanner.Where(s => s.Page == "Our MPs").FirstOrDefault();
            return View(ourMps);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditMPs(OurMps OM)
        {

            if (OM.Image != null && OM.Image.ContentLength > 0)
            {
                OM.ImageExtension = Path.GetExtension(OM.Image.FileName).Trim('.');
                OM.ImageName = Path.GetFileNameWithoutExtension(OM.Image.FileName);
                OM.ImageGUID = Guid.NewGuid().ToString("N");
                string Imagelocation = Server.MapPath("~/Resources/Images/OurMps/" + OM.ImageGUID + "." + OM.ImageExtension);
                OM.Image.SaveAs(Imagelocation);

            }
            Int64 Result = _aboutUsDao.SaveOurMps(OM);
            if (Result > 0)
            {
                return RedirectToRoute("OurMPs");
            }

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOurMps(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                Int64 Draft = _aboutUsDao.DeleteOurMps(GUID, UserId, SystemIp);
                return RedirectToRoute("OurMPs", new { EncryptedId = string.Empty });
            }
            return View();
        }


        public ActionResult SortOurMps()
        {
            IList<OurMps> resources = _aboutUsDao.GetOurMpsSorting();
            return View(resources);
        }



        [HttpPost]
        public ActionResult SortOurMps(List<Sorting> sort)
        {
            List<Sorting> sorting = new List<Sorting>();


            if (sort != null && sort.Count > 0)
            {
                foreach (Sorting so in sort)
                {
                    Sorting s = new Sorting();
                    if (so.GUID != null && so.GUID != Guid.Empty)//
                    {
                        s.GUID = so.GUID;
                        s.ReOrder = so.ReOrder;
                        sorting.Add(s);
                    }
                }
                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Int32 result = _aboutUsDao.UpdateOurMpsSorting(sorting, UserId, SystemIp);
            }
            TempData["data"] = "Changes Updated";
            return RedirectToAction("OurMPs");
        }

        #endregion



        public ActionResult OrganisationChart()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            TownMapPDF town = _aboutUsDao.GetOrganisationChartPDF();
            return View(town);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrganisationChart(TownMapPDF T)
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

            Int64 Id = _aboutUsDao.SaveOrganisationChartPDF(T);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("OrganisationChart");
            }
            return View();
        }

        public ActionResult ViewPublications()
        {
            OurPublicationsViewModel viewModel = _aboutUsDao.GetOurPublications();
            return View(viewModel);
        }


        public ActionResult AddAnnualReports()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnnualReports(AnnualReports AU)
        {
            if (AU.LargeImage != null && AU.LargeImage.ContentLength > 0)
            {
                AU.ImageExtension = Path.GetExtension(AU.LargeImage.FileName).Trim('.');
                AU.ImageName = Path.GetFileNameWithoutExtension(AU.LargeImage.FileName);
                AU.ImageGUID = Guid.NewGuid().ToString("N");
                string LargeImagelocation = Server.MapPath("~/Resources/Images/AboutUs/" + AU.ImageGUID + "." + AU.ImageExtension);
                AU.LargeImage.SaveAs(LargeImagelocation);
            }

            if (AU.FileName != null && AU.FileName.ContentLength > 0)
            {
                AU.PDFExtension = Path.GetExtension(AU.FileName.FileName).Trim('.');
                AU.PDFName = Path.GetFileNameWithoutExtension(AU.FileName.FileName);
                AU.PDFGUID = Guid.NewGuid().ToString("N");
                double PDFFileSize = (AU.FileName.ContentLength / 1048576.00);
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/AboutUs" + AU.PDFGUID + "." + AU.PDFExtension);
                AU.FileName.SaveAs(PdfFileLocation);
                AU.PDFFileSize = Convert.ToString(PDFFileSize);


            }


            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            AU.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            if (_aboutUsDao.Save(AU, new Guid(GUID)) > 0)
            {
                return RedirectToAction("ViewPublications");
            }
            return PartialView();
        }

        public ActionResult EditAnnualReports(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                AnnualReports AU = _aboutUsDao.GetbyGuid(!string.IsNullOrEmpty(EncryptedId) ? new Guid(EncryptedId) : Guid.Empty);
                return PartialView(AU);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnnualReports(AnnualReports AU)
        {
            if (AU.LargeImage != null && AU.LargeImage.ContentLength > 0)
            {
                AU.ImageExtension = Path.GetExtension(AU.LargeImage.FileName).Trim('.');
                AU.ImageName = Path.GetFileNameWithoutExtension(AU.LargeImage.FileName);
                AU.ImageGUID = Guid.NewGuid().ToString("N");
                string LargeImagelocation = Server.MapPath("~/Resources/Images/AboutUs/" + AU.ImageGUID + "." + AU.ImageExtension);
                AU.LargeImage.SaveAs(LargeImagelocation);
            }

            if (AU.FileName != null && AU.FileName.ContentLength > 0)
            {
                AU.PDFExtension = Path.GetExtension(AU.FileName.FileName).Trim('.');
                AU.PDFName = Path.GetFileNameWithoutExtension(AU.FileName.FileName);
                AU.PDFGUID = Guid.NewGuid().ToString("N");
                double PDFFileSize = (AU.FileName.ContentLength / 1048576.00);
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/AboutUs" + AU.PDFGUID + "." + AU.PDFExtension);
                AU.FileName.SaveAs(PdfFileLocation);
                AU.PDFFileSize = Convert.ToString(PDFFileSize);


            }


            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            AU.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            if (_aboutUsDao.Save(AU, new Guid(GUID)) > 0)
            {
                return RedirectToAction("ViewPublications");
            }

            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnnualReports()
        {
            return View();
        }

        public ActionResult AddPressRelease()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPressRelease(PressRelease PR)
        {
            if (PR.FileName != null && PR.FileName.ContentLength > 0)
            {
                PR.PDFExtension = Path.GetExtension(PR.FileName.FileName).Trim('.');
                PR.PDFName = Path.GetFileNameWithoutExtension(PR.FileName.FileName);
                PR.PDFGUID = Guid.NewGuid().ToString("N");
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/PressRelease/" + PR.PDFGUID + "." + PR.PDFExtension);
                PR.FileName.SaveAs(PdfFileLocation);
            }

            DateTime SDate;
            DateTime.TryParseExact(PR.Date, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out SDate);
            PR.PressDate = SDate;
            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            PR.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            if (_aboutUsDao.SavePressRelease(PR, new Guid(GUID)) > 0)
            {
                return RedirectToAction("ViewPublications");
            }


            return PartialView();
        }

        public ActionResult EditPressRelease(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                PressRelease AU = _aboutUsDao.GetbyGuidPress(!string.IsNullOrEmpty(EncryptedId) ? new Guid(EncryptedId) : Guid.Empty);
                return PartialView(AU);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPressRelease(PressRelease PR)
        {
            if (PR.FileName != null && PR.FileName.ContentLength > 0)
            {
                PR.PDFExtension = Path.GetExtension(PR.FileName.FileName).Trim('.');
                PR.PDFName = Path.GetFileNameWithoutExtension(PR.FileName.FileName);
                PR.PDFGUID = Guid.NewGuid().ToString("N");
                string PdfFileLocation = Server.MapPath("~/Resources/Documents/PressRelease/" + PR.PDFGUID + "." + PR.PDFExtension);
                PR.FileName.SaveAs(PdfFileLocation);
            }

            DateTime SDate;
            DateTime.TryParseExact(PR.Date, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out SDate);
            PR.PressDate = SDate;
            string GUID = ((UserAccount)Session["UserAccount"]).GUID.ToString();
            PR.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
            if (_aboutUsDao.SavePressRelease(PR, new Guid(GUID)) > 0)
            {
                return RedirectToAction("ViewPublications");
            }

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePressRelease()
        {
            return View();
        }




    }
}