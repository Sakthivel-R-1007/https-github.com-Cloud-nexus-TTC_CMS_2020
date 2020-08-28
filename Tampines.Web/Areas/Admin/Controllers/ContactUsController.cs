using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines.Web.Filters;
using Tampines_CMS.Persistence.Interface;
using Tampines_CMS.Domain;
using Tampines.Web.Helpers;
using System.IO;

namespace Tampines.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
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
        // GET: Admin/ContactUs
        public ActionResult UsefulLinksContacts()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            List<LinksContacts> LinksContacts = _contactUsDao.Get(1, 10);
            return View(LinksContacts);

        }

        public ActionResult PartialViewLinksContents(int PageIndex)
        {
            List<LinksContacts> LinksContacts = _contactUsDao.Get(PageIndex, 10);
            return View(LinksContacts);
        }
        public ActionResult AddLinksContacts()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddLinksContacts(LinksContacts LC)
        {
            Int64 Result = _contactUsDao.SaveLinkContacts(LC);
            if (Result > 0)
            {
                return RedirectToAction("UsefulLinksContacts");
            }
            return View();
        }
        public ActionResult EditLinksContacts(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                LinksContacts linksContacts = _contactUsDao.GetEditLinkContacts(new Guid(EncryptedId));
                return View(linksContacts);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLinksContact(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                Int64 Draft = _contactUsDao.DeleteLinksContact(GUID, UserId, SystemIp);
                return RedirectToRoute("UsefulLinksContacts", new { EncryptedId = string.Empty });
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Preview(LinksContacts LC)
        {
            LinksContacts linksContacts = LC;
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();
            if (viewBanner != null && viewBanner.ContactUsBanner != null && viewBanner.ContactUsBanner.Count > 0)
                linksContacts.ContactUsBanner = viewBanner.ContactUsBanner.Where(s => s.Page == "Useful Links & Contacts").FirstOrDefault();
            return View(linksContacts);
        }

        public ActionResult SortLinksContacts()
        {
            IList<LinksContacts> resources = _contactUsDao.GetResourcesSorting();
            return View(resources);
        }


        [HttpPost]
        public ActionResult SortLinksContacts(List<Sorting> sort)
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
                Int32 result = _contactUsDao.UpdateResourcesSorting(sorting, UserId, SystemIp);
            }
            TempData["data"] = "Changes Updated";
            return RedirectToAction("UsefulLinksContacts");
        }
      


        public ActionResult PDPAPublicPolicy()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            TownMapPDF town = _contactUsDao.GetPDPAPDF();
            return View(town);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PDPAPublicPolicy(TownMapPDF T)
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

            Int64 Id = _contactUsDao.SavePDPAPDF(T);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("PDPAPublicPolicy");
            }
            return View();
        }

      


        public ActionResult WhistleBlowingPolicy()
        {
            if (TempData["data"] != null)
            {
                ViewBag.DataBaseStatus = "Changes Updated";
            }
            TownMapPDF town = _contactUsDao.GetWhistleBlowingPolicyPDF();
            return View(town);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WhistleBlowingPolicy(TownMapPDF T)
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

            Int64 Id = _contactUsDao.SaveWhistleBlowingPolicyPDF(T);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("WhistleBlowingPolicy");
            }
            return View();
        }
        public ActionResult ViewFAQ()
        {
            IList<FAQ> fAQs = _contactUsDao.GetFAQ(1,10);
            return View(fAQs);
        }

        public ActionResult AddQuestion()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion(FAQ fAQ)
        {
            fAQ.UserId = ((UserAccount)Session["UserAccount"]).GUID;
            fAQ.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

            Int64 Id = _contactUsDao.SaveFAQ(fAQ);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("ViewFAQ");
            }
            return View();
        }

        public ActionResult EditQuestion(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                FAQ fAQ = _contactUsDao.GetFAQByGuid(new Guid(EncryptedId));
                return View(fAQ);
            }
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(FAQ fAQ)
        {
            fAQ.UserId = ((UserAccount)Session["UserAccount"]).GUID;
            fAQ.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

            Int64 Id = _contactUsDao.SaveFAQ(fAQ);
            if (Id > 0)
            {
                TempData["data"] = "Success";
                return RedirectToAction("ViewFAQ");
            }
            return View();
        }


        public ActionResult FAQPreview()
        {
            return RedirectToAction("ViewFAQ");
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult FAQPreview(FAQ fAQ)
        {
           
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();          
            if (viewBanner != null && viewBanner.ContactUsBanner != null && viewBanner.ContactUsBanner.Count > 0)
                fAQ.ContactUsBanner = viewBanner.ContactUsBanner.Where(s => s.Page == "FAQs").FirstOrDefault();
            return View(fAQ);
        }

        public ActionResult Subscription()
        {
            return View();
        }


    }
}