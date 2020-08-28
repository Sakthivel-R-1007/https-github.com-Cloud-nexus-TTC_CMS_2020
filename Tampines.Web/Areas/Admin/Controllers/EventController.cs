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
    public class EventController : Controller
    {

        #region Constructor And Private Members

        private IEventsDao _EventsDao;


        public EventController(IEventsDao EventsDao)
        {
            _EventsDao = EventsDao;

        }

        #endregion
        // GET: Admin/Event
        public ActionResult Index()
        {
            IList<Events> Events = _EventsDao.Get(1, 10);
            return View(Events);
        }

        public ActionResult PartialVieweEvents(int PageIndex)
        {
            IList<Events> Events = _EventsDao.Get(PageIndex, 10);
            return View(Events);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Events EV)
        {

            if (EV.Image != null && EV.Image.ContentLength > 0)
            {
                EV.ImageExtension = Path.GetExtension(EV.Image.FileName).Trim('.');
                EV.ImageName = Path.GetFileNameWithoutExtension(EV.Image.FileName);
                EV.ImageGUID = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/Events/" + EV.ImageGUID + "." + EV.ImageExtension);
                EV.Image.SaveAs(ThumbnailImagelocation);

                DateTime SDate;
                DateTime.TryParseExact(EV.Date, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out SDate);
                EV.EventDate = SDate;

            }

            var Result = _EventsDao.SaveEvents(EV);
            if (Result > 0)
            {
                return RedirectToRoute("EventIndex");
            }
            return View();
        }


        public ActionResult Edit(string EncDetail)
        {
            if(!string.IsNullOrEmpty(EncDetail))
            {
                Events events = _EventsDao.GetEventByGuid(new Guid(EncDetail));
                return View(events);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Events EV)
        {

            if (EV.Image != null && EV.Image.ContentLength > 0)
            {
                EV.ImageExtension = Path.GetExtension(EV.Image.FileName).Trim('.');
                EV.ImageName = Path.GetFileNameWithoutExtension(EV.Image.FileName);
                EV.ImageGUID = Guid.NewGuid().ToString("N");
                string ThumbnailImagelocation = Server.MapPath("~/Resources/Images/Events/" + EV.ImageGUID + "." + EV.ImageExtension);
                EV.Image.SaveAs(ThumbnailImagelocation);

            }

            if (!string.IsNullOrEmpty(EV.Date))
            {
                DateTime SDate;
                DateTime.TryParseExact(EV.Date, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out SDate);
                EV.EventDate = SDate;
            }

            var Result = _EventsDao.SaveEvents(EV);
            if (Result > 0)
            {
                return RedirectToRoute("EventIndex");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvents(string EncryptedId)
        {
            if (!string.IsNullOrEmpty(EncryptedId))
            {
                Guid GUID = new Guid(EncryptedId);
                Guid UserId = ((UserAccount)Session["UserAccount"]).GUID;
                string SystemIp = GetRemoteIp.GetIPAddress(HttpContext);
                Int64 Id = _EventsDao.DeleteEvents(GUID, UserId, SystemIp);
                return RedirectToRoute("EventIndex");
            }
            return View();
        }


        public JsonResult CheckEventTitle(string EventTitle, string EncDetail)
        {

            int result = CheckNewsTit(EventTitle, EncDetail);
            return Json(result == 0, JsonRequestBehavior.AllowGet);
        }

        public int CheckNewsTit(string EventTitle, string EncDetail)
        {
            string Title = (!string.IsNullOrEmpty(EventTitle) ? EventTitle : null);
            Guid GUID = !string.IsNullOrEmpty(EncDetail) && EncDetail != "null" ? new Guid(EncDetail) : Guid.Empty;
            int result = _EventsDao.CheckEventTitleName(Title, GUID);
            return result;
        }
    }
}